using System;

namespace MsConsumers.Domain.Entities;

public class ConsumerAddress
{
    public Guid Id { get; private set; }
    public Guid ConsumerId { get; private set; }
    public string StreetAddress { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string State { get; private set; } = string.Empty;
    public string PostalCode { get; private set; } = string.Empty;
    public double? Latitude { get; private set; }
    public double? Longitude { get; private set; }
    public bool IsDefault { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public Guid CountryId { get; private set; }

    // Navigation properties
    public Consumer Consumer { get; private set; } = null!;
    public CountryCode Country { get; private set; } = null!;

    private ConsumerAddress() { }

    public ConsumerAddress(
        Guid id,
        Guid consumerId,
        string streetAddress,
        string city,
        string state,
        string postalCode,
        double? latitude,
        double? longitude,
        bool isDefault,
        Guid countryId,
        Consumer consumer,
        CountryCode country)
    {
        Id = id;
        ConsumerId = consumerId;
        StreetAddress = streetAddress;
        City = city;
        State = state;
        PostalCode = postalCode;
        Latitude = latitude;
        Longitude = longitude;
        IsDefault = isDefault;
        CountryId = countryId;
        Consumer = consumer;
        Country = country;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Update(
        string streetAddress,
        string city,
        string state,
        string postalCode,
        Guid countryId,
        bool isDefault,
        double? latitude = null,
        double? longitude = null)
    {
        StreetAddress = streetAddress;
        City = city;
        State = state;
        PostalCode = postalCode;
        CountryId = countryId;
        IsDefault = isDefault;
        Latitude = latitude;
        Longitude = longitude;
        UpdatedAt = DateTime.UtcNow;
    }
} 