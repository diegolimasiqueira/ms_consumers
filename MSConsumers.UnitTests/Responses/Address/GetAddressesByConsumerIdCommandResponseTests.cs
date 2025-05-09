using System;
using MSConsumers.Application.Commands.Address;
using Xunit;

namespace MSConsumers.UnitTests.Responses.Address;

public class GetAddressesByConsumerIdCommandResponseTests
{
    [Fact]
    public void Constructor_ShouldInitializePropertiesWithDefaultValues()
    {
        // Arrange & Act
        var response = new GetAddressesByConsumerIdCommandResponse();

        // Assert
        Assert.Equal(Guid.Empty, response.Id);
        Assert.Equal(Guid.Empty, response.ConsumerId);
        Assert.Equal(string.Empty, response.ZipCode);
        Assert.Equal(string.Empty, response.Street);
        Assert.Equal(string.Empty, response.Number);
        Assert.Null(response.Complement);
        Assert.Equal(string.Empty, response.Neighborhood);
        Assert.Equal(string.Empty, response.City);
        Assert.Equal(string.Empty, response.State);
        Assert.Equal(string.Empty, response.Country);
        Assert.False(response.IsMain);
        Assert.Equal(DateTime.MinValue, response.CreatedAt);
        Assert.Equal(DateTime.MinValue, response.UpdatedAt);
    }

    [Fact]
    public void Properties_ShouldBeSettable()
    {
        // Arrange
        var id = Guid.NewGuid();
        var consumerId = Guid.NewGuid();
        var zipCode = "12345-678";
        var street = "Rua Teste";
        var number = "123";
        var complement = "Apto 4B";
        var neighborhood = "Centro";
        var city = "São Paulo";
        var state = "SP";
        var country = "Brasil";
        var isMain = true;
        var createdAt = DateTime.UtcNow.AddDays(-1);
        var updatedAt = DateTime.UtcNow;

        // Act
        var response = new GetAddressesByConsumerIdCommandResponse
        {
            Id = id,
            ConsumerId = consumerId,
            ZipCode = zipCode,
            Street = street,
            Number = number,
            Complement = complement,
            Neighborhood = neighborhood,
            City = city,
            State = state,
            Country = country,
            IsMain = isMain,
            CreatedAt = createdAt,
            UpdatedAt = updatedAt
        };

        // Assert
        Assert.Equal(id, response.Id);
        Assert.Equal(consumerId, response.ConsumerId);
        Assert.Equal(zipCode, response.ZipCode);
        Assert.Equal(street, response.Street);
        Assert.Equal(number, response.Number);
        Assert.Equal(complement, response.Complement);
        Assert.Equal(neighborhood, response.Neighborhood);
        Assert.Equal(city, response.City);
        Assert.Equal(state, response.State);
        Assert.Equal(country, response.Country);
        Assert.Equal(isMain, response.IsMain);
        Assert.Equal(createdAt, response.CreatedAt);
        Assert.Equal(updatedAt, response.UpdatedAt);
    }
} 