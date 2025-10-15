using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VirtualClass.Core.Repository;
using VirtualClass.Core.Services;
using VirtualClass.Infrastructure.Auth;
using VirtualClass.Infrastructure.Persistence;
using VirtualClass.Infrastructure.Persistence.Repository;

namespace VirtualClass.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<VirtualClassDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Connection"),
                                    a => a.MigrationsAssembly(typeof(VirtualClassDbContext).Assembly.FullName)));

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }   
    }
}
