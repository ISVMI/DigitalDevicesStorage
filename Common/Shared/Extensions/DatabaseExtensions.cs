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
        /// <summary>
        /// Method version without IServiceProvider as a part of initializer (version without messaging)
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="serviceProvider"></param>
        /// <param name="initializer"></param>
        /// <returns></returns>
        public static async Task InitializeDatabaseAsync<TContext>(
            this IServiceProvider serviceProvider,
            Func<TContext, Task>? initializer = null)
            where TContext : DbContext
        {
            await serviceProvider.InitializeDatabaseAsync<TContext>(
                async (sp, context) =>
                {
                    if (initializer != null)
                        await initializer(context);
                });
        }

        /// <summary>
        /// Overload version of an Initialization method for a messaging support (through IServiceProvider requirement)
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="serviceProvider"></param>
        /// <param name="initializer"></param>
        /// <returns></returns>
        public static async Task InitializeDatabaseAsync<TContext>(
            this IServiceProvider serviceProvider,
            Func<IServiceProvider, TContext, Task>? initializer = null)
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
                    await initializer(services, context);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Database initialization failure: {ex.Message}");
            }
        }
    }
}
