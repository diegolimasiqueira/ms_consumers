using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MsConsumers.Infrastructure.Data;
using MsConsumers.Shared.Configurations;

namespace MsConsumers.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseSettings = configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>();
        
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(
                databaseSettings.GetConnectionString(),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        return services;
    }
} 