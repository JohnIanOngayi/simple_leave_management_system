using Microsoft.EntityFrameworkCore;
using simple_leave_management_system.Infrastructure;
using simple_leave_management_system.Infrastructure.Repository;

namespace simple_leave_management_system.Services
{
    public static class ServiceExtensions
    {
        public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<RepositoryContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        }

        public static void ConfigureMsSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<RepositoryContext>(options =>
            options.UseSqlServer(connectionString)); // This requires Microsoft.EntityFrameworkCore.SqlServer
        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }
    }
}
