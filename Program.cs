using Bogus;
using DigitalDevices.AuthApp;
using DigitalDevices.DataContext;
using DigitalDevices.DataSeeding;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContextFactory<DigitalDevicesContext, DigitalDevicesContextFactory>(options=>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<RolesSeeder>();
builder.Services.AddResponseCompression(options => options.EnableForHttps = true);

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequiredLength = 6;
    options.Stores.ProtectPersonalData = false;
    options.Lockout.AllowedForNewUsers = false;
})
.AddEntityFrameworkStores<DigitalDevicesContext>()
.AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
    options.Password.RequiredLength = 6);

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromHours(4);
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.SlidingExpiration = true;
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
        policy.RequireRole("Admin"));

    options.AddPolicy("ProductsManagementPolicy", policy =>
        policy.RequireRole("Admin", "Manager"));
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseResponseCaching();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var contextFactory = services.GetRequiredService<DigitalDevicesContextFactory>();
        await contextFactory.CreateDbContext().Database.MigrateAsync();


        var rolesSeeder = services.GetRequiredService<RolesSeeder>();

        await DbInitializer.InitializeAsync(contextFactory.CreateDbContext(), rolesSeeder);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ошибка инициализации базы данных");
    }
}

app.Run();