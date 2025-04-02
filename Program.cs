using Bogus;
using DigitalDevices.AuthApp;
using DigitalDevices.DataContext;
using DigitalDevices.DataSeeding;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DigitalDevicesContext>();

builder.Services.AddResponseCompression(options => options.EnableForHttps = true);

builder.Services.AddIdentity<User, IdentityRole>(options => {
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<DigitalDevicesContext>()
.AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
    options.Password.RequiredLength = 6);
builder.Services.AddScoped<RolesSeeder>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromHours(4);
    options.LoginPath = "/Accounts/Login";
    options.LogoutPath = "/Accounts/Logout";
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
        var context = services.GetRequiredService<DigitalDevicesContext>();
        await context.Database.MigrateAsync();

        var rolesSeeder = services.GetRequiredService<RolesSeeder>();
        await rolesSeeder.SeedRolesAsync();

        await DbInitializer.InitializeAsync(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ошибка инициализации базы данных");
    }
}

app.Run();