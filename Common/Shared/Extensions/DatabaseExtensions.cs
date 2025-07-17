using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Extensions
{
    public static class DatabaseExtensions
    {
        public static void AddDatabaseService<TContext>(
            this IServiceCollection services,
            IConfiguration configuration,
            string connectionString = "DefaultConnection")
            where TContext : DbContext
        {
            services.AddDbContext<TContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString(connectionString)));
        }

        public static async Task InitializeDatabaseAsync<TContext>(
            this IServiceProvider serviceProvider,
            Func<TContext, Task>? initializer = null)
            where TContext : DbContext
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<TContext>();

                    await context.Database.MigrateAsync();

                if (initializer != null)
                {
                    await initializer(context);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка инициализации базы данных {ex.Message}");
            }
        }
    }
}
