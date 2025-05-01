using System;
using MsConsumers.Domain.Entities;
using Xunit;

namespace MsConsumers.UnitTests.Entities;

public class AddressEntityTests
{
    [Fact]
    public void Constructor_WithValidParameters_ShouldCreateAddress()
    {
        // Arrange
        var consumerId = Guid.NewGuid();
        var countryId = Guid.NewGuid();
        var streetAddress = "Rua Teste, 123";
        var city = "São Paulo";
        var state = "SP";
        var postalCode = "12345-678";
        var latitude = 23.5505;
        var longitude = 46.6333;
        var isDefault = true;

        // Act
        var address = new AddressEntity(
            consumerId,
            streetAddress,
            city,
            state,
            postalCode,
            latitude,
            longitude,
            isDefault,
            countryId);

        // Assert
        Assert.NotEqual(Guid.Empty, address.Id);
        Assert.Equal(consumerId, address.ConsumerId);
        Assert.Equal(streetAddress, address.StreetAddress);
        Assert.Equal(city, address.City);
        Assert.Equal(state, address.State);
        Assert.Equal(postalCode, address.PostalCode);
        Assert.Equal(latitude, address.Latitude);
        Assert.Equal(longitude, address.Longitude);
        Assert.Equal(isDefault, address.IsDefault);
        Assert.Equal(countryId, address.CountryId);
        Assert.True(DateTime.UtcNow.Subtract(address.CreatedAt).TotalSeconds <= 1);
        Assert.True(DateTime.UtcNow.Subtract(address.UpdatedAt).TotalSeconds <= 1);
    }

    [Fact]
    public void Update_WithValidParameters_ShouldUpdateAddress()
    {
        // Arrange
        var address = new AddressEntity(
            Guid.NewGuid(),
            "Rua Antiga, 123",
            "São Paulo",
            "SP",
            "12345-678",
            23.5505,
            46.6333,
            true,
            Guid.NewGuid());

        var newStreetAddress = "Rua Nova, 456";
        var newCity = "Rio de Janeiro";
        var newState = "RJ";
        var newPostalCode = "98765-432";
        var newLatitude = 22.9068;
        var newLongitude = 43.1729;
        var newIsDefault = false;
        var newCountryId = Guid.NewGuid();

        // Act
        address.Update(
            newStreetAddress,
            newCity,
            newState,
            newPostalCode,
            newLatitude,
            newLongitude,
            newIsDefault,
            newCountryId);

        // Assert
        Assert.Equal(newStreetAddress, address.StreetAddress);
        Assert.Equal(newCity, address.City);
        Assert.Equal(newState, address.State);
        Assert.Equal(newPostalCode, address.PostalCode);
        Assert.Equal(newLatitude, address.Latitude);
        Assert.Equal(newLongitude, address.Longitude);
        Assert.Equal(newIsDefault, address.IsDefault);
        Assert.Equal(newCountryId, address.CountryId);
        Assert.True(DateTime.UtcNow.Subtract(address.UpdatedAt).TotalSeconds <= 1);
    }
} 