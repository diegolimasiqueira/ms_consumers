using System;

namespace MsConsumers.Domain.Entities;

public class ConsumerAddress
{
    public Guid Id { get; private set; }
    public Guid ConsumerId { get; private set; }
    public string StreetAddress { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string PostalCode { get; private set; }
    public double? Latitude { get; private set; }
    public double? Longitude { get; private set; }
    public bool IsDefault { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public Guid CountryId { get; private set; }

    // Navigation properties
    public virtual Consumer Consumer { get; private set; }
    public virtual CountryCode Country { get; private set; }

    protected ConsumerAddress() { }

    public ConsumerAddress(
        Guid consumerId,
        string streetAddress,
        string city,
        string state,
        string postalCode,
        Guid countryId,
        bool isDefault = false,
        double? latitude = null,
        double? longitude = null)
    {
        Id = Guid.NewGuid();
        ConsumerId = consumerId;
        StreetAddress = streetAddress;
        City = city;
        State = state;
        PostalCode = postalCode;
        CountryId = countryId;
        IsDefault = isDefault;
        Latitude = latitude;
        Longitude = longitude;
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