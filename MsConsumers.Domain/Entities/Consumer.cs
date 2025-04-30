using System;

namespace MsConsumers.Domain.Entities;

public class Consumer
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string DocumentId { get; private set; }
    public string? PhotoUrl { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public Guid CurrencyId { get; private set; }
    public Guid PhoneCountryCodeId { get; private set; }
    public Guid PreferredLanguageId { get; private set; }
    public Guid TimezoneId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    // Navigation properties
    public virtual Currency Currency { get; private set; }
    public virtual CountryCode PhoneCountryCode { get; private set; }
    public virtual Language PreferredLanguage { get; private set; }
    public virtual TimeZone Timezone { get; private set; }
    public virtual ICollection<ConsumerAddress> Addresses { get; private set; }

    protected Consumer() { }

    public Consumer(
        string name,
        string documentId,
        string phoneNumber,
        string email,
        Guid currencyId,
        Guid phoneCountryCodeId,
        Guid preferredLanguageId,
        Guid timezoneId,
        string? photoUrl = null)
    {
        Id = Guid.NewGuid();
        Name = name;
        DocumentId = documentId;
        PhoneNumber = phoneNumber;
        Email = email;
        CurrencyId = currencyId;
        PhoneCountryCodeId = phoneCountryCodeId;
        PreferredLanguageId = preferredLanguageId;
        TimezoneId = timezoneId;
        PhotoUrl = photoUrl;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        Addresses = new List<ConsumerAddress>();
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