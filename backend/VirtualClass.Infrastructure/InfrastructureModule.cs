using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VirtualClass.Infrastructure.Persistence;

namespace VirtualClass.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<VirtualClassDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Connection"),
                                    a => a.MigrationsAssembly(typeof(VirtualClassDbContext).Assembly.FullName)));

            return services;
        }   
    }
}
