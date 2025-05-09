using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MSConsumers.Domain.Entities;
using MSConsumers.Infrastructure.Data;
using Xunit;

namespace MSConsumers.UnitTests.Infrastructure.Data;

public class ApplicationDbContextTests
{
    private readonly DbContextOptions<ApplicationDbContext> _options;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextTests()
    {
        _options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationDbContext(_options);
    }

    [Fact]
    public void ConsumerEntity_ShouldHaveCorrectTableAndSchemaName()
    {
        // Act
        var entityType = _context.Model.FindEntityType(typeof(ConsumerEntity));

        // Assert
        Assert.Equal("tb_consumers", entityType.GetTableName());
        Assert.Equal("shc_consumer", entityType.GetSchema());
    }

    [Fact]
    public void ConsumerEntity_ShouldHaveRequiredProperties()
    {
        // Act
        var entityType = _context.Model.FindEntityType(typeof(ConsumerEntity));

        // Assert
        Assert.False(entityType.FindProperty(nameof(ConsumerEntity.Id)).IsNullable);
        Assert.False(entityType.FindProperty(nameof(ConsumerEntity.Name)).IsNullable);
        Assert.False(entityType.FindProperty(nameof(ConsumerEntity.DocumentId)).IsNullable);
        Assert.False(entityType.FindProperty(nameof(ConsumerEntity.PhoneNumber)).IsNullable);
        Assert.False(entityType.FindProperty(nameof(ConsumerEntity.Email)).IsNullable);
        Assert.False(entityType.FindProperty(nameof(ConsumerEntity.CurrencyId)).IsNullable);
        Assert.False(entityType.FindProperty(nameof(ConsumerEntity.PhoneCountryCodeId)).IsNullable);
        Assert.False(entityType.FindProperty(nameof(ConsumerEntity.PreferredLanguageId)).IsNullable);
        Assert.False(entityType.FindProperty(nameof(ConsumerEntity.TimezoneId)).IsNullable);
        Assert.False(entityType.FindProperty(nameof(ConsumerEntity.CreatedAt)).IsNullable);
        Assert.False(entityType.FindProperty(nameof(ConsumerEntity.UpdatedAt)).IsNullable);
    }

    [Fact]
    public void ConsumerEntity_ShouldHaveCorrectMaxLengths()
    {
        // Act
        var entityType = _context.Model.FindEntityType(typeof(ConsumerEntity));

        // Assert
        Assert.Equal(100, entityType.FindProperty(nameof(ConsumerEntity.Name)).GetMaxLength());
        Assert.Equal(50, entityType.FindProperty(nameof(ConsumerEntity.DocumentId)).GetMaxLength());
        Assert.Equal(500, entityType.FindProperty(nameof(ConsumerEntity.PhotoUrl)).GetMaxLength());
        Assert.Equal(20, entityType.FindProperty(nameof(ConsumerEntity.PhoneNumber)).GetMaxLength());
        Assert.Equal(255, entityType.FindProperty(nameof(ConsumerEntity.Email)).GetMaxLength());
    }

    [Fact]
    public void ConsumerEntity_ShouldHaveUniqueIndexes()
    {
        // Act
        var entityType = _context.Model.FindEntityType(typeof(ConsumerEntity));
        var documentIdIndex = entityType.FindIndex(entityType.FindProperty(nameof(ConsumerEntity.DocumentId)));
        var emailIndex = entityType.FindIndex(entityType.FindProperty(nameof(ConsumerEntity.Email)));
        var phoneNumberIndex = entityType.FindIndex(entityType.FindProperty(nameof(ConsumerEntity.PhoneNumber)));

        // Assert
        Assert.True(documentIdIndex.IsUnique);
        Assert.True(emailIndex.IsUnique);
        Assert.True(phoneNumberIndex.IsUnique);
    }

    [Fact]
    public void CountryCode_ShouldHaveCorrectTableAndSchemaName()
    {
        // Act
        var entityType = _context.Model.FindEntityType(typeof(CountryCode));

        // Assert
        Assert.Equal("tb_country_codes", entityType.GetTableName());
        Assert.Equal("shc_consumer", entityType.GetSchema());
    }

    [Fact]
    public void CountryCode_ShouldHaveRequiredProperties()
    {
        // Act
        var entityType = _context.Model.FindEntityType(typeof(CountryCode));

        // Assert
        Assert.False(entityType.FindProperty(nameof(CountryCode.Id)).IsNullable);
        Assert.False(entityType.FindProperty(nameof(CountryCode.Code)).IsNullable);
        Assert.False(entityType.FindProperty(nameof(CountryCode.CountryName)).IsNullable);
    }

    [Fact]
    public void CountryCode_ShouldHaveCorrectMaxLengths()
    {
        // Act
        var entityType = _context.Model.FindEntityType(typeof(CountryCode));

        // Assert
        Assert.Equal(5, entityType.FindProperty(nameof(CountryCode.Code)).GetMaxLength());
        Assert.Equal(100, entityType.FindProperty(nameof(CountryCode.CountryName)).GetMaxLength());
    }

    [Fact]
    public void CountryCode_ShouldHaveUniqueIndex()
    {
        // Act
        var entityType = _context.Model.FindEntityType(typeof(CountryCode));
        var codeIndex = entityType.FindIndex(entityType.FindProperty(nameof(CountryCode.Code)));

        // Assert
        Assert.True(codeIndex.IsUnique);
    }

    [Fact]
    public void Currency_ShouldHaveCorrectTableAndSchemaName()
    {
        // Act
        var entityType = _context.Model.FindEntityType(typeof(Currency));

        // Assert
        Assert.Equal("tb_currencies", entityType.GetTableName());
        Assert.Equal("shc_consumer", entityType.GetSchema());
    }

    [Fact]
    public void Currency_ShouldHaveRequiredProperties()
    {
        // Act
        var entityType = _context.Model.FindEntityType(typeof(Currency));

        // Assert
        Assert.False(entityType.FindProperty(nameof(Currency.Id)).IsNullable);
        Assert.False(entityType.FindProperty(nameof(Currency.Code)).IsNullable);
        Assert.False(entityType.FindProperty(nameof(Currency.Description)).IsNullable);
    }

    [Fact]
    public void Currency_ShouldHaveCorrectMaxLengths()
    {
        // Act
        var entityType = _context.Model.FindEntityType(typeof(Currency));

        // Assert
        Assert.Equal(3, entityType.FindProperty(nameof(Currency.Code)).GetMaxLength());
        Assert.Equal(100, entityType.FindProperty(nameof(Currency.Description)).GetMaxLength());
    }

    [Fact]
    public void Currency_ShouldHaveUniqueIndex()
    {
        // Act
        var entityType = _context.Model.FindEntityType(typeof(Currency));
        var codeIndex = entityType.FindIndex(entityType.FindProperty(nameof(Currency.Code)));

        // Assert
        Assert.True(codeIndex.IsUnique);
    }

    [Fact]
    public void Language_ShouldHaveCorrectTableAndSchemaName()
    {
        // Act
        var entityType = _context.Model.FindEntityType(typeof(Language));

        // Assert
        Assert.Equal("tb_languages", entityType.GetTableName());
        Assert.Equal("shc_consumer", entityType.GetSchema());
    }

    [Fact]
    public void Language_ShouldHaveRequiredProperties()
    {
        // Act
        var entityType = _context.Model.FindEntityType(typeof(Language));

        // Assert
        Assert.False(entityType.FindProperty(nameof(Language.Id)).IsNullable);
        Assert.False(entityType.FindProperty(nameof(Language.Code)).IsNullable);
        Assert.False(entityType.FindProperty(nameof(Language.Description)).IsNullable);
    }

    [Fact]
    public void Language_ShouldHaveCorrectMaxLengths()
    {
        // Act
        var entityType = _context.Model.FindEntityType(typeof(Language));

        // Assert
        Assert.Equal(10, entityType.FindProperty(nameof(Language.Code)).GetMaxLength());
        Assert.Equal(100, entityType.FindProperty(nameof(Language.Description)).GetMaxLength());
    }

    [Fact]
    public void Language_ShouldHaveUniqueIndex()
    {
        // Act
        var entityType = _context.Model.FindEntityType(typeof(Language));
        var codeIndex = entityType.FindIndex(entityType.FindProperty(nameof(Language.Code)));

        // Assert
        Assert.True(codeIndex.IsUnique);
    }

    [Fact]
    public void TimeZone_ShouldHaveCorrectTableAndSchemaName()
    {
        // Act
        var entityType = _context.Model.FindEntityType(typeof(MSConsumers.Domain.Entities.TimeZone));

        // Assert
        Assert.Equal("tb_time_zones", entityType.GetTableName());
        Assert.Equal("shc_consumer", entityType.GetSchema());
    }

    [Fact]
    public void TimeZone_ShouldHaveRequiredProperties()
    {
        // Act
        var entityType = _context.Model.FindEntityType(typeof(MSConsumers.Domain.Entities.TimeZone));

        // Assert
        Assert.False(entityType.FindProperty(nameof(MSConsumers.Domain.Entities.TimeZone.Id)).IsNullable);
        Assert.False(entityType.FindProperty(nameof(MSConsumers.Domain.Entities.TimeZone.Name)).IsNullable);
        Assert.False(entityType.FindProperty(nameof(MSConsumers.Domain.Entities.TimeZone.Description)).IsNullable);
    }

    [Fact]
    public void TimeZone_ShouldHaveCorrectMaxLengths()
    {
        // Act
        var entityType = _context.Model.FindEntityType(typeof(MSConsumers.Domain.Entities.TimeZone));

        // Assert
        Assert.Equal(50, entityType.FindProperty(nameof(MSConsumers.Domain.Entities.TimeZone.Name)).GetMaxLength());
        Assert.Equal(100, entityType.FindProperty(nameof(MSConsumers.Domain.Entities.TimeZone.Description)).GetMaxLength());
    }

    [Fact]
    public void TimeZone_ShouldHaveUniqueIndex()
    {
        // Act
        var entityType = _context.Model.FindEntityType(typeof(MSConsumers.Domain.Entities.TimeZone));
        var nameIndex = entityType.FindIndex(entityType.FindProperty(nameof(MSConsumers.Domain.Entities.TimeZone.Name)));

        // Assert
        Assert.True(nameIndex.IsUnique);
    }
} 