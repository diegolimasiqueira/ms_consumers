using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSConsumers.Domain.Interfaces;
using MSConsumers.Infrastructure.Configurations;
using MSConsumers.Infrastructure.Data;
using MSConsumers.Infrastructure.Repositories;

namespace MSConsumers.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseSettings = configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>();
        
        if (databaseSettings == null)
            throw new InvalidOperationException("DatabaseSettings section is missing in configuration");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(
                databaseSettings.GetConnectionString(),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddScoped<IConsumerRepository, ConsumerRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();

        return services;
    }
} 