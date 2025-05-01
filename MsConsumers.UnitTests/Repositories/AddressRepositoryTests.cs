using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MsConsumers.Domain.Entities;
using MsConsumers.Infrastructure.Data;
using MsConsumers.Infrastructure.Data.Configurations;
using MsConsumers.Infrastructure.Repositories;
using Xunit;

namespace MsConsumers.UnitTests.Repositories;

public class AddressRepositoryTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly AddressRepository _repository;
    private readonly ConsumerEntity _consumer;
    private readonly CountryCode _country;

    public AddressRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationDbContext(options);
        _context.Database.EnsureCreated();
        _repository = new AddressRepository(_context);

        // Criar entidades relacionadas
        _consumer = new ConsumerEntity(
            "Test Consumer",
            "12345678900",
            null,
            "11999999999",
            "test@test.com",
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid());

        _country = new CountryCode(Guid.NewGuid(), "BR", "Brazil");

        _context.Consumers.Add(_consumer);
        _context.CountryCodes.Add(_country);
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Fact]
    public async Task AddAsync_ShouldAddAddress()
    {
        // Arrange
        var address = new AddressEntity(
            _consumer.Id,
            "Rua Teste, 123",
            "São Paulo",
            "SP",
            "12345-678",
            23.5505,
            46.6333,
            true,
            _country.Id);

        // Act
        await _repository.AddAsync(address);

        // Assert
        var savedAddress = await _context.Addresses
            .Include(a => a.ConsumerEntity)
            .Include(a => a.Country)
            .FirstOrDefaultAsync(a => a.Id == address.Id);
        
        Assert.NotNull(savedAddress);
        Assert.Equal(address.StreetAddress, savedAddress!.StreetAddress);
        Assert.NotNull(savedAddress.ConsumerEntity);
        Assert.NotNull(savedAddress.Country);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateAddress()
    {
        // Arrange
        var address = new AddressEntity(
            _consumer.Id,
            "Rua Antiga, 123",
            "São Paulo",
            "SP",
            "12345-678",
            23.5505,
            46.6333,
            true,
            _country.Id);

        await _repository.AddAsync(address);

        // Act
        address.Update(
            "Rua Nova, 456",
            "Rio de Janeiro",
            "RJ",
            "98765-432",
            22.9068,
            43.1729,
            false,
            _country.Id);

        await _repository.UpdateAsync(address);

        // Assert
        var updatedAddress = await _context.Addresses.FindAsync(address.Id);
        Assert.NotNull(updatedAddress);
        Assert.Equal("Rua Nova, 456", updatedAddress!.StreetAddress);
        Assert.Equal("Rio de Janeiro", updatedAddress.City);
        Assert.Equal("RJ", updatedAddress.State);
        Assert.Equal("98765-432", updatedAddress.PostalCode);
        Assert.Equal(22.9068, updatedAddress.Latitude);
        Assert.Equal(43.1729, updatedAddress.Longitude);
        Assert.False(updatedAddress.IsDefault);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveAddress()
    {
        // Arrange
        var address = new AddressEntity(
            _consumer.Id,
            "Rua Teste, 123",
            "São Paulo",
            "SP",
            "12345-678",
            23.5505,
            46.6333,
            true,
            _country.Id);

        await _repository.AddAsync(address);

        // Act
        await _repository.DeleteAsync(address);

        // Assert
        var deletedAddress = await _context.Addresses.FindAsync(address.Id);
        Assert.Null(deletedAddress);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnAddress()
    {
        // Arrange
        var address = new AddressEntity(
            _consumer.Id,
            "Rua Teste, 123",
            "São Paulo",
            "SP",
            "12345-678",
            23.5505,
            46.6333,
            true,
            _country.Id);

        await _repository.AddAsync(address);

        // Act
        var result = await _repository.GetByIdAsync(address.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(address.Id, result!.Id);
        Assert.Equal(address.StreetAddress, result.StreetAddress);
        Assert.Equal(address.City, result.City);
        Assert.Equal(address.State, result.State);
        Assert.Equal(address.PostalCode, result.PostalCode);
        Assert.Equal(address.Latitude, result.Latitude);
        Assert.Equal(address.Longitude, result.Longitude);
        Assert.Equal(address.IsDefault, result.IsDefault);
        Assert.Equal(address.CountryId, result.CountryId);
    }

    [Fact]
    public async Task GetByConsumerIdAsync_ShouldReturnAddresses()
    {
        // Arrange
        var address1 = new AddressEntity(
                _consumer.Id,
            "Rua Teste 1, 123",
                "São Paulo",
                "SP",
                "12345-678",
                23.5505,
                46.6333,
                true,
            _country.Id);

        var address2 = new AddressEntity(
                _consumer.Id,
            "Rua Teste 2, 456",
                "São Paulo",
                "SP",
                "12345-678",
                23.5505,
                46.6333,
                false,
            _country.Id);

        await _repository.AddAsync(address1);
        await _repository.AddAsync(address2);

        // Act
        var result = await _repository.GetByConsumerIdAsync(_consumer.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Contains(result, a => a.Id == address1.Id);
        Assert.Contains(result, a => a.Id == address2.Id);
    }

    [Fact]
    public async Task GetMainAddressByConsumerIdAsync_ShouldReturnDefaultAddress()
    {
        // Arrange
        var address1 = new AddressEntity(
                _consumer.Id,
            "Rua Teste 1, 123",
                "São Paulo",
                "SP",
                "12345-678",
                23.5505,
                46.6333,
            true,
            _country.Id);

        var address2 = new AddressEntity(
                _consumer.Id,
            "Rua Teste 2, 456",
                "São Paulo",
                "SP",
                "12345-678",
                23.5505,
                46.6333,
            false,
            _country.Id);

        await _repository.AddAsync(address1);
        await _repository.AddAsync(address2);

        // Act
        var result = await _repository.GetMainAddressByConsumerIdAsync(_consumer.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(address1.Id, result!.Id);
        Assert.True(result.IsDefault);
    }
} 