using System;
using System.Collections.Generic;

namespace MsConsumers.Domain.Entities;

public class Consumer
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string DocumentId { get; private set; } = string.Empty;
    public string? PhotoUrl { get; private set; }
    public string PhoneNumber { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public Guid CurrencyId { get; private set; }
    public Guid PhoneCountryCodeId { get; private set; }
    public Guid PreferredLanguageId { get; private set; }
    public Guid TimezoneId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    // Navigation properties
    public Currency Currency { get; private set; } = null!;
    public CountryCode PhoneCountryCode { get; private set; } = null!;
    public Language PreferredLanguage { get; private set; } = null!;
    public TimeZone Timezone { get; private set; } = null!;
    public ICollection<ConsumerAddress> Addresses { get; private set; } = new List<ConsumerAddress>();

    private Consumer() { }

    public Consumer(
        Guid id,
        string name,
        string documentId,
        string? photoUrl,
        string phoneNumber,
        string email,
        Guid currencyId,
        Guid phoneCountryCodeId,
        Guid preferredLanguageId,
        Guid timezoneId,
        Currency currency,
        CountryCode phoneCountryCode,
        Language preferredLanguage,
        TimeZone timezone)
    {
        Id = id;
        Name = name;
        DocumentId = documentId;
        PhotoUrl = photoUrl;
        PhoneNumber = phoneNumber;
        Email = email;
        CurrencyId = currencyId;
        PhoneCountryCodeId = phoneCountryCodeId;
        PreferredLanguageId = preferredLanguageId;
        TimezoneId = timezoneId;
        Currency = currency;
        PhoneCountryCode = phoneCountryCode;
        PreferredLanguage = preferredLanguage;
        Timezone = timezone;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Update(
        string name,
        string phoneNumber,
        string email,
        Guid currencyId,
        Guid phoneCountryCodeId,
        Guid preferredLanguageId,
        Guid timezoneId,
        string? photoUrl = null)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        Email = email;
        CurrencyId = currencyId;
        PhoneCountryCodeId = phoneCountryCodeId;
        PreferredLanguageId = preferredLanguageId;
        TimezoneId = timezoneId;
        PhotoUrl = photoUrl;
        UpdatedAt = DateTime.UtcNow;
    }
} 