using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MsConsumers.Domain.Interfaces;
using MsConsumers.Infrastructure.Data;
using MsConsumers.Infrastructure.Extensions;
using MsConsumers.Infrastructure.Repositories;
using Xunit;

namespace MsConsumers.UnitTests.Infrastructure.Extensions;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddInfrastructure_WithValidConfiguration_ShouldRegisterServices()
    {
        // Arrange
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        // Act
        services.AddInfrastructure(configuration);
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        
        // Verifica se o DbContext foi registrado
        var dbContext = serviceProvider.GetService<ApplicationDbContext>();
        Assert.NotNull(dbContext);

        // Verifica se os repositórios foram registrados
        var consumerRepository = serviceProvider.GetService<IConsumerRepository>();
        Assert.NotNull(consumerRepository);
        Assert.IsType<ConsumerRepository>(consumerRepository);

        var addressRepository = serviceProvider.GetService<IAddressRepository>();
        Assert.NotNull(addressRepository);
        Assert.IsType<AddressRepository>(addressRepository);
    }

    [Fact]
    public void AddInfrastructure_WithInvalidConfiguration_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>())
            .Build();

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => 
            services.AddInfrastructure(configuration));
        
        Assert.Equal("DatabaseSettings section is missing in configuration", exception.Message);
    }

    [Fact]
    public void AddInfrastructure_ShouldConfigureDbContextWithCorrectOptions()
    {
        // Arrange
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        // Act
        services.AddInfrastructure(configuration);
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var dbContext = serviceProvider.GetService<ApplicationDbContext>();
        
        Assert.NotNull(dbContext);
        Assert.Equal("MsConsumers.Infrastructure", dbContext.GetType().Assembly.GetName().Name);
    }
} 