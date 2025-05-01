using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace MsConsumers.Domain.Entities
{
    public class ConsumerEntity
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

        // Propriedades de navegação
        public Currency? Currency { get; private set; }
        public CountryCode? PhoneCountryCode { get; private set; }
        public Language? PreferredLanguage { get; private set; }
        public TimeZone? Timezone { get; private set; }
        public ICollection<AddressEntity> Addresses { get; private set; } = new List<AddressEntity>();

        public ConsumerEntity(
            string name,
            string documentId,
            string? photoUrl,
            string phoneNumber,
            string email,
            Guid currencyId,
            Guid phoneCountryCodeId,
            Guid preferredLanguageId,
            Guid timezoneId)
        {
            ValidateName(name);
            ValidateDocumentId(documentId);
            ValidatePhotoUrl(photoUrl);
            ValidatePhoneNumber(phoneNumber);
            ValidateEmail(email);
            ValidateIds(currencyId, phoneCountryCodeId, preferredLanguageId, timezoneId);

            Id = Guid.NewGuid();
            Name = name;
            DocumentId = documentId;
            PhotoUrl = photoUrl;
            PhoneNumber = phoneNumber;
            Email = email;
            CurrencyId = currencyId;
            PhoneCountryCodeId = phoneCountryCodeId;
            PreferredLanguageId = preferredLanguageId;
            TimezoneId = timezoneId;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Update(
            string name,
            string documentId,
            string? photoUrl,
            string phoneNumber,
            string email,
            Guid currencyId,
            Guid phoneCountryCodeId,
            Guid preferredLanguageId,
            Guid timezoneId)
        {
            ValidateName(name);
            ValidateDocumentId(documentId);
            ValidatePhotoUrl(photoUrl);
            ValidatePhoneNumber(phoneNumber);
            ValidateEmail(email);
            ValidateIds(currencyId, phoneCountryCodeId, preferredLanguageId, timezoneId);

            Name = name;
            DocumentId = documentId;
            PhotoUrl = photoUrl;
            PhoneNumber = phoneNumber;
            Email = email;
            CurrencyId = currencyId;
            PhoneCountryCodeId = phoneCountryCodeId;
            PreferredLanguageId = preferredLanguageId;
            TimezoneId = timezoneId;
            UpdatedAt = DateTime.UtcNow;
        }

        private void ValidateName(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name), "Name cannot be null");
            
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty", nameof(name));
            
            if (name.Length > 100)
                throw new ArgumentException("Name cannot be longer than 100 characters", nameof(name));
        }

        private void ValidateDocumentId(string documentId)
        {
            if (documentId == null)
                throw new ArgumentNullException(nameof(documentId), "Document ID cannot be null");
            
            if (string.IsNullOrWhiteSpace(documentId))
                throw new ArgumentException("Document ID cannot be empty", nameof(documentId));
            
            if (documentId.Length > 50)
                throw new ArgumentException("Document ID cannot be longer than 50 characters", nameof(documentId));
        }

        private void ValidatePhotoUrl(string? photoUrl)
        {
            if (photoUrl != null && photoUrl.Length > 500)
                throw new ArgumentException("Photo URL cannot be longer than 500 characters", nameof(photoUrl));
        }

        private void ValidatePhoneNumber(string phoneNumber)
        {
            if (phoneNumber == null)
                throw new ArgumentNullException(nameof(phoneNumber), "Phone number cannot be null");
            
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Phone number cannot be empty", nameof(phoneNumber));
            
            if (phoneNumber.Length > 20)
                throw new ArgumentException("Phone number cannot be longer than 20 characters", nameof(phoneNumber));

            var phoneRegex = new Regex(@"^\+?[1-9]\d{7,14}$");
            if (!phoneRegex.IsMatch(phoneNumber))
                throw new ArgumentException("Invalid phone number format", nameof(phoneNumber));
        }

        private void ValidateEmail(string email)
        {
            if (email == null)
                throw new ArgumentNullException(nameof(email), "Email cannot be null");
            
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty", nameof(email));
            
            if (email.Length > 255)
                throw new ArgumentException("Email cannot be longer than 255 characters", nameof(email));

            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!emailRegex.IsMatch(email))
                throw new ArgumentException("Invalid email format", nameof(email));
        }

        private void ValidateIds(Guid currencyId, Guid phoneCountryCodeId, Guid preferredLanguageId, Guid timezoneId)
        {
            if (currencyId == Guid.Empty)
                throw new ArgumentException("Currency ID cannot be empty", nameof(currencyId));
            
            if (phoneCountryCodeId == Guid.Empty)
                throw new ArgumentException("Phone country code ID cannot be empty", nameof(phoneCountryCodeId));
            
            if (preferredLanguageId == Guid.Empty)
                throw new ArgumentException("Preferred language ID cannot be empty", nameof(preferredLanguageId));
            
            if (timezoneId == Guid.Empty)
                throw new ArgumentException("Timezone ID cannot be empty", nameof(timezoneId));
        }
    }
} 