using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MsConsumers.Domain.Entities;
using MsConsumers.Infrastructure.Context;
using Xunit;

namespace MsConsumers.UnitTests.Infrastructure.Context;

public class MsConsumersDbContextTests
{
    private readonly DbContextOptions<MsConsumersDbContext> _options;
    private readonly MsConsumersDbContext _context;

    public MsConsumersDbContextTests()
    {
        _options = new DbContextOptionsBuilder<MsConsumersDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new MsConsumersDbContext(_options);
    }

    [Fact]
    public void ConsumerEntity_ShouldBeConfigured()
    {
        // Act
        var entityType = _context.Model.FindEntityType(typeof(ConsumerEntity));

        // Assert
        Assert.NotNull(entityType);
        Assert.Equal("tb_consumers", entityType.GetTableName());
        Assert.Equal("shc_consumer", entityType.GetSchema());
    }

    [Fact]
    public void AddressEntity_ShouldBeConfigured()
    {
        // Act
        var entityType = _context.Model.FindEntityType(typeof(AddressEntity));

        // Assert
        Assert.NotNull(entityType);
        Assert.Equal("tb_consumer_address", entityType.GetTableName());
        Assert.Equal("shc_consumer", entityType.GetSchema());
    }

    [Fact]
    public void OnModelCreating_ShouldApplyConfigurationsFromAssembly()
    {
        // Act
        var model = _context.Model;

        // Assert
        Assert.NotNull(model);
        Assert.True(model.GetEntityTypes().Count() > 0);
    }

    [Fact]
    public void Constructor_WithValidOptions_ShouldInitializeContext()
    {
        // Act
        var context = new MsConsumersDbContext(_options);

        // Assert
        Assert.NotNull(context);
        Assert.NotNull(context.Consumers);
        Assert.NotNull(context.Addresses);
    }
} 