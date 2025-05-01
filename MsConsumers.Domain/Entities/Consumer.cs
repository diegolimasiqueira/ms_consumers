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
    public ICollection<AddressEntity> Addresses { get; private set; } = new List<AddressEntity>();

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
        ValidateRequiredFields(name, documentId, phoneNumber, email);
        ValidateIds(id, currencyId, phoneCountryCodeId, preferredLanguageId, timezoneId);
        ValidateNavigationProperties(currency, phoneCountryCode, preferredLanguage, timezone);

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
        string? photoUrl,
        string phoneNumber,
        string email,
        Guid currencyId,
        Guid phoneCountryCodeId,
        Guid preferredLanguageId,
        Guid timezoneId)
    {
        ValidateRequiredFields(name, phoneNumber, email);
        ValidateUpdateIds(currencyId, phoneCountryCodeId, preferredLanguageId, timezoneId);

        Name = name;
        PhotoUrl = photoUrl;
        PhoneNumber = phoneNumber;
        Email = email;
        CurrencyId = currencyId;
        PhoneCountryCodeId = phoneCountryCodeId;
        PreferredLanguageId = preferredLanguageId;
        TimezoneId = timezoneId;
        UpdatedAt = DateTime.UtcNow;
    }

    private static void ValidateRequiredFields(string name, string documentId, string phoneNumber, string email)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty", nameof(name));

        if (string.IsNullOrWhiteSpace(documentId))
            throw new ArgumentException("DocumentId cannot be null or empty", nameof(documentId));

        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("PhoneNumber cannot be null or empty", nameof(phoneNumber));

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be null or empty", nameof(email));
    }

    private static void ValidateRequiredFields(string name, string phoneNumber, string email)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty", nameof(name));

        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("PhoneNumber cannot be null or empty", nameof(phoneNumber));

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be null or empty", nameof(email));
    }

    private static void ValidateIds(Guid id, Guid currencyId, Guid phoneCountryCodeId, Guid preferredLanguageId, Guid timezoneId)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id cannot be empty", nameof(id));

        ValidateUpdateIds(currencyId, phoneCountryCodeId, preferredLanguageId, timezoneId);
    }

    private static void ValidateUpdateIds(Guid currencyId, Guid phoneCountryCodeId, Guid preferredLanguageId, Guid timezoneId)
    {
        if (currencyId == Guid.Empty)
            throw new ArgumentException("CurrencyId cannot be empty", nameof(currencyId));

        if (phoneCountryCodeId == Guid.Empty)
            throw new ArgumentException("PhoneCountryCodeId cannot be empty", nameof(phoneCountryCodeId));

        if (preferredLanguageId == Guid.Empty)
            throw new ArgumentException("PreferredLanguageId cannot be empty", nameof(preferredLanguageId));

        if (timezoneId == Guid.Empty)
            throw new ArgumentException("TimezoneId cannot be empty", nameof(timezoneId));
    }

    private static void ValidateNavigationProperties(Currency currency, CountryCode phoneCountryCode, Language preferredLanguage, TimeZone timezone)
    {
        if (currency == null)
            throw new ArgumentNullException(nameof(currency));

        if (phoneCountryCode == null)
            throw new ArgumentNullException(nameof(phoneCountryCode));

        if (preferredLanguage == null)
            throw new ArgumentNullException(nameof(preferredLanguage));

        if (timezone == null)
            throw new ArgumentNullException(nameof(timezone));
    }
} 