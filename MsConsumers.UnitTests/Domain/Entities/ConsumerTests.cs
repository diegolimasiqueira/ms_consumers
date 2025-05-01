using System;
using Xunit;
using MsConsumers.Domain.Entities;

namespace MsConsumers.UnitTests.Domain.Entities
{
    public class ConsumerTests
    {
        private readonly Guid _validId = Guid.NewGuid();
        private readonly string _validName = "Test Consumer";
        private readonly string _validDocumentId = "12345678900";
        private readonly string _validPhotoUrl = "https://example.com/photo.jpg";
        private readonly string _validPhoneNumber = "11999999999";
        private readonly string _validEmail = "test@test.com";
        private readonly Guid _validCurrencyId = Guid.NewGuid();
        private readonly Guid _validPhoneCountryCodeId = Guid.NewGuid();
        private readonly Guid _validPreferredLanguageId = Guid.NewGuid();
        private readonly Guid _validTimezoneId = Guid.NewGuid();

        private readonly Currency _validCurrency;
        private readonly CountryCode _validPhoneCountryCode;
        private readonly Language _validPreferredLanguage;
        private readonly MsConsumers.Domain.Entities.TimeZone _validTimezone;

        public ConsumerTests()
        {
            _validCurrency = new Currency(_validCurrencyId, "USD", "US Dollar");
            _validPhoneCountryCode = new CountryCode(_validPhoneCountryCodeId, "BR", "Brazil");
            _validPreferredLanguage = new Language(_validPreferredLanguageId, "en", "English");
            _validTimezone = new MsConsumers.Domain.Entities.TimeZone(_validTimezoneId, "UTC", "Coordinated Universal Time");
        }

        [Fact]
        public void Constructor_WithValidData_ShouldCreateConsumer()
        {
            // Act
            var consumer = new Consumer(
                _validId,
                _validName,
                _validDocumentId,
                _validPhotoUrl,
                _validPhoneNumber,
                _validEmail,
                _validCurrencyId,
                _validPhoneCountryCodeId,
                _validPreferredLanguageId,
                _validTimezoneId,
                _validCurrency,
                _validPhoneCountryCode,
                _validPreferredLanguage,
                _validTimezone);

            // Assert
            Assert.Equal(_validId, consumer.Id);
            Assert.Equal(_validName, consumer.Name);
            Assert.Equal(_validDocumentId, consumer.DocumentId);
            Assert.Equal(_validPhotoUrl, consumer.PhotoUrl);
            Assert.Equal(_validPhoneNumber, consumer.PhoneNumber);
            Assert.Equal(_validEmail, consumer.Email);
            Assert.Equal(_validCurrencyId, consumer.CurrencyId);
            Assert.Equal(_validPhoneCountryCodeId, consumer.PhoneCountryCodeId);
            Assert.Equal(_validPreferredLanguageId, consumer.PreferredLanguageId);
            Assert.Equal(_validTimezoneId, consumer.TimezoneId);
            Assert.Equal(_validCurrency, consumer.Currency);
            Assert.Equal(_validPhoneCountryCode, consumer.PhoneCountryCode);
            Assert.Equal(_validPreferredLanguage, consumer.PreferredLanguage);
            Assert.Equal(_validTimezone, consumer.Timezone);
            Assert.NotEqual(default, consumer.CreatedAt);
            Assert.NotEqual(default, consumer.UpdatedAt);
            Assert.NotNull(consumer.Addresses);
            Assert.Empty(consumer.Addresses);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Constructor_WithInvalidName_ShouldThrowArgumentException(string invalidName)
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new Consumer(
                _validId,
                invalidName,
                _validDocumentId,
                _validPhotoUrl,
                _validPhoneNumber,
                _validEmail,
                _validCurrencyId,
                _validPhoneCountryCodeId,
                _validPreferredLanguageId,
                _validTimezoneId,
                _validCurrency,
                _validPhoneCountryCode,
                _validPreferredLanguage,
                _validTimezone));
            Assert.Equal("name", exception.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Constructor_WithInvalidDocumentId_ShouldThrowArgumentException(string invalidDocumentId)
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new Consumer(
                _validId,
                _validName,
                invalidDocumentId,
                _validPhotoUrl,
                _validPhoneNumber,
                _validEmail,
                _validCurrencyId,
                _validPhoneCountryCodeId,
                _validPreferredLanguageId,
                _validTimezoneId,
                _validCurrency,
                _validPhoneCountryCode,
                _validPreferredLanguage,
                _validTimezone));
            Assert.Equal("documentId", exception.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Constructor_WithInvalidPhoneNumber_ShouldThrowArgumentException(string invalidPhoneNumber)
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new Consumer(
                _validId,
                _validName,
                _validDocumentId,
                _validPhotoUrl,
                invalidPhoneNumber,
                _validEmail,
                _validCurrencyId,
                _validPhoneCountryCodeId,
                _validPreferredLanguageId,
                _validTimezoneId,
                _validCurrency,
                _validPhoneCountryCode,
                _validPreferredLanguage,
                _validTimezone));
            Assert.Equal("phoneNumber", exception.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Constructor_WithInvalidEmail_ShouldThrowArgumentException(string invalidEmail)
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new Consumer(
                _validId,
                _validName,
                _validDocumentId,
                _validPhotoUrl,
                _validPhoneNumber,
                invalidEmail,
                _validCurrencyId,
                _validPhoneCountryCodeId,
                _validPreferredLanguageId,
                _validTimezoneId,
                _validCurrency,
                _validPhoneCountryCode,
                _validPreferredLanguage,
                _validTimezone));
            Assert.Equal("email", exception.ParamName);
        }

        [Fact]
        public void Constructor_WithEmptyGuidIds_ShouldThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new Consumer(
                Guid.Empty,
                _validName,
                _validDocumentId,
                _validPhotoUrl,
                _validPhoneNumber,
                _validEmail,
                Guid.Empty,
                Guid.Empty,
                Guid.Empty,
                Guid.Empty,
                _validCurrency,
                _validPhoneCountryCode,
                _validPreferredLanguage,
                _validTimezone));
            Assert.Equal("id", exception.ParamName);
        }

        [Fact]
        public void Update_WithValidData_ShouldUpdateConsumer()
        {
            // Arrange
            var consumer = CreateValidConsumer();
            var newName = "Updated Consumer";
            var newPhotoUrl = "https://example.com/new-photo.jpg";
            var newPhoneNumber = "11988888888";
            var newEmail = "updated@test.com";
            var newCurrencyId = Guid.NewGuid();
            var newPhoneCountryCodeId = Guid.NewGuid();
            var newPreferredLanguageId = Guid.NewGuid();
            var newTimezoneId = Guid.NewGuid();

            // Act
            consumer.Update(
                newName,
                newPhotoUrl,
                newPhoneNumber,
                newEmail,
                newCurrencyId,
                newPhoneCountryCodeId,
                newPreferredLanguageId,
                newTimezoneId);

            // Assert
            Assert.Equal(newName, consumer.Name);
            Assert.Equal(newPhotoUrl, consumer.PhotoUrl);
            Assert.Equal(newPhoneNumber, consumer.PhoneNumber);
            Assert.Equal(newEmail, consumer.Email);
            Assert.Equal(newCurrencyId, consumer.CurrencyId);
            Assert.Equal(newPhoneCountryCodeId, consumer.PhoneCountryCodeId);
            Assert.Equal(newPreferredLanguageId, consumer.PreferredLanguageId);
            Assert.Equal(newTimezoneId, consumer.TimezoneId);
            Assert.NotEqual(default, consumer.UpdatedAt);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Update_WithInvalidName_ShouldThrowArgumentException(string invalidName)
        {
            // Arrange
            var consumer = CreateValidConsumer();

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => consumer.Update(
                invalidName,
                _validPhotoUrl,
                _validPhoneNumber,
                _validEmail,
                _validCurrencyId,
                _validPhoneCountryCodeId,
                _validPreferredLanguageId,
                _validTimezoneId));
            Assert.Equal("name", exception.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Update_WithInvalidPhoneNumber_ShouldThrowArgumentException(string invalidPhoneNumber)
        {
            // Arrange
            var consumer = CreateValidConsumer();

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => consumer.Update(
                _validName,
                _validPhotoUrl,
                invalidPhoneNumber,
                _validEmail,
                _validCurrencyId,
                _validPhoneCountryCodeId,
                _validPreferredLanguageId,
                _validTimezoneId));
            Assert.Equal("phoneNumber", exception.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Update_WithInvalidEmail_ShouldThrowArgumentException(string invalidEmail)
        {
            // Arrange
            var consumer = CreateValidConsumer();

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => consumer.Update(
                _validName,
                _validPhotoUrl,
                _validPhoneNumber,
                invalidEmail,
                _validCurrencyId,
                _validPhoneCountryCodeId,
                _validPreferredLanguageId,
                _validTimezoneId));
            Assert.Equal("email", exception.ParamName);
        }

        [Fact]
        public void Update_WithEmptyGuidIds_ShouldThrowArgumentException()
        {
            // Arrange
            var consumer = CreateValidConsumer();

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => consumer.Update(
                _validName,
                _validPhotoUrl,
                _validPhoneNumber,
                _validEmail,
                Guid.Empty,
                Guid.Empty,
                Guid.Empty,
                Guid.Empty));
            Assert.Equal("currencyId", exception.ParamName);
        }

        private Consumer CreateValidConsumer()
        {
            return new Consumer(
                _validId,
                _validName,
                _validDocumentId,
                _validPhotoUrl,
                _validPhoneNumber,
                _validEmail,
                _validCurrencyId,
                _validPhoneCountryCodeId,
                _validPreferredLanguageId,
                _validTimezoneId,
                _validCurrency,
                _validPhoneCountryCode,
                _validPreferredLanguage,
                _validTimezone);
        }
    }
} 