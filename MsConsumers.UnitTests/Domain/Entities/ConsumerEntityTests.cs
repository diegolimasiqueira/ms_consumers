using System;
using Xunit;
using MsConsumers.Domain.Entities;
using System.Collections.Generic;

namespace MsConsumers.UnitTests.Domain.Entities
{
    public class ConsumerEntityTests
    {
        private readonly Guid _validCurrencyId = Guid.NewGuid();
        private readonly Guid _validPhoneCountryCodeId = Guid.NewGuid();
        private readonly Guid _validPreferredLanguageId = Guid.NewGuid();
        private readonly Guid _validTimezoneId = Guid.NewGuid();

        [Fact]
        public void Constructor_WithValidData_ShouldCreateConsumer()
        {
            // Arrange
            var name = "Test Consumer";
            var documentId = "12345678900";
            var photoUrl = "https://example.com/photo.jpg";
            var phoneNumber = "11999999999";
            var email = "test@test.com";

            // Act
            var consumer = new ConsumerEntity(
                name,
                documentId,
                photoUrl,
                phoneNumber,
                email,
                _validCurrencyId,
                _validPhoneCountryCodeId,
                _validPreferredLanguageId,
                _validTimezoneId);

            // Assert
            Assert.NotEqual(Guid.Empty, consumer.Id);
            Assert.Equal(name, consumer.Name);
            Assert.Equal(documentId, consumer.DocumentId);
            Assert.Equal(photoUrl, consumer.PhotoUrl);
            Assert.Equal(phoneNumber, consumer.PhoneNumber);
            Assert.Equal(email, consumer.Email);
            Assert.Equal(_validCurrencyId, consumer.CurrencyId);
            Assert.Equal(_validPhoneCountryCodeId, consumer.PhoneCountryCodeId);
            Assert.Equal(_validPreferredLanguageId, consumer.PreferredLanguageId);
            Assert.Equal(_validTimezoneId, consumer.TimezoneId);
            Assert.NotEqual(default, consumer.CreatedAt);
            Assert.NotEqual(default, consumer.UpdatedAt);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        public void Constructor_WithInvalidName_ShouldThrowArgumentException(string invalidName)
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => new ConsumerEntity(
                invalidName,
                "12345678900",
                "https://example.com/photo.jpg",
                "11999999999",
                "test@test.com",
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid()));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        public void Constructor_WithInvalidDocumentId_ShouldThrowArgumentException(string invalidDocumentId)
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => new ConsumerEntity(
                "Test Consumer",
                invalidDocumentId,
                "https://example.com/photo.jpg",
                "11999999999",
                "test@test.com",
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid()));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        public void Constructor_WithInvalidPhoneNumber_ShouldThrowArgumentException(string invalidPhoneNumber)
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => new ConsumerEntity(
                "Test Consumer",
                "12345678900",
                "https://example.com/photo.jpg",
                invalidPhoneNumber,
                "test@test.com",
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid()));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        [InlineData("invalid-email")]
        [InlineData("test@")]
        [InlineData("@test.com")]
        public void Constructor_WithInvalidEmail_ShouldThrowArgumentException(string invalidEmail)
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => new ConsumerEntity(
                "Test Consumer",
                "12345678900",
                "https://example.com/photo.jpg",
                "11999999999",
                invalidEmail,
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid()));
        }

        [Fact]
        public void Constructor_WithNameTooLong_ShouldThrowException()
        {
            // Arrange
            var name = new string('a', 101); // 101 characters
            var documentId = "12345678900";
            var photoUrl = "https://example.com/photo.jpg";
            var phoneNumber = "11999999999";
            var email = "test@test.com";

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new ConsumerEntity(
                name,
                documentId,
                photoUrl,
                phoneNumber,
                email,
                _validCurrencyId,
                _validPhoneCountryCodeId,
                _validPreferredLanguageId,
                _validTimezoneId));

            Assert.Equal("name", exception.ParamName);
        }

        [Theory]
        [InlineData(null)]
        public void Constructor_WithNullDocumentId_ShouldThrowArgumentNullException(string? invalidDocumentId)
        {
            // Arrange
            var name = "Test Consumer";
            var photoUrl = "https://example.com/photo.jpg";
            var phoneNumber = "11999999999";
            var email = "test@test.com";

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new ConsumerEntity(
                name,
                invalidDocumentId!,
                photoUrl,
                phoneNumber,
                email,
                _validCurrencyId,
                _validPhoneCountryCodeId,
                _validPreferredLanguageId,
                _validTimezoneId));

            Assert.Equal("documentId", exception.ParamName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        public void Constructor_WithEmptyDocumentId_ShouldThrowArgumentException(string invalidDocumentId)
        {
            // Arrange
            var name = "Test Consumer";
            var photoUrl = "https://example.com/photo.jpg";
            var phoneNumber = "11999999999";
            var email = "test@test.com";

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new ConsumerEntity(
                name,
                invalidDocumentId,
                photoUrl,
                phoneNumber,
                email,
                _validCurrencyId,
                _validPhoneCountryCodeId,
                _validPreferredLanguageId,
                _validTimezoneId));

            Assert.Equal("documentId", exception.ParamName);
        }

        [Theory]
        [InlineData(null)]
        public void Constructor_WithNullPhoneNumber_ShouldThrowArgumentNullException(string? invalidPhoneNumber)
        {
            // Arrange
            var name = "Test Consumer";
            var documentId = "12345678900";
            var photoUrl = "https://example.com/photo.jpg";
            var email = "test@test.com";

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new ConsumerEntity(
                name,
                documentId,
                photoUrl,
                invalidPhoneNumber!,
                email,
                _validCurrencyId,
                _validPhoneCountryCodeId,
                _validPreferredLanguageId,
                _validTimezoneId));

            Assert.Equal("phoneNumber", exception.ParamName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        public void Constructor_WithEmptyPhoneNumber_ShouldThrowArgumentException(string invalidPhoneNumber)
        {
            // Arrange
            var name = "Test Consumer";
            var documentId = "12345678900";
            var photoUrl = "https://example.com/photo.jpg";
            var email = "test@test.com";

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new ConsumerEntity(
                name,
                documentId,
                photoUrl,
                invalidPhoneNumber,
                email,
                _validCurrencyId,
                _validPhoneCountryCodeId,
                _validPreferredLanguageId,
                _validTimezoneId));

            Assert.Equal("phoneNumber", exception.ParamName);
        }

        [Theory]
        [InlineData("abc")]
        public void Constructor_WithInvalidPhoneNumberFormat_ShouldThrowArgumentException(string invalidPhoneNumber)
        {
            // Arrange
            var name = "Test Consumer";
            var documentId = "12345678900";
            var photoUrl = "https://example.com/photo.jpg";
            var email = "test@test.com";

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new ConsumerEntity(
                name,
                documentId,
                photoUrl,
                invalidPhoneNumber,
                email,
                _validCurrencyId,
                _validPhoneCountryCodeId,
                _validPreferredLanguageId,
                _validTimezoneId));

            Assert.Equal("phoneNumber", exception.ParamName);
        }

        [Theory]
        [InlineData(null)]
        public void Constructor_WithNullEmail_ShouldThrowArgumentNullException(string? invalidEmail)
        {
            // Arrange
            var name = "Test Consumer";
            var documentId = "12345678900";
            var photoUrl = "https://example.com/photo.jpg";
            var phoneNumber = "11999999999";

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new ConsumerEntity(
                name,
                documentId,
                photoUrl,
                phoneNumber,
                invalidEmail!,
                _validCurrencyId,
                _validPhoneCountryCodeId,
                _validPreferredLanguageId,
                _validTimezoneId));

            Assert.Equal("email", exception.ParamName);
        }

        [Fact]
        public void Constructor_WithEmptyGuidIds_ShouldThrowException()
        {
            // Arrange
            var name = "Test Consumer";
            var documentId = "12345678900";
            var photoUrl = "https://example.com/photo.jpg";
            var phoneNumber = "11999999999";
            var email = "test@test.com";

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new ConsumerEntity(
                name,
                documentId,
                photoUrl,
                phoneNumber,
                email,
                Guid.Empty,
                Guid.Empty,
                Guid.Empty,
                Guid.Empty));

            Assert.Equal("currencyId", exception.ParamName);
        }

        [Fact]
        public void Update_WithValidData_ShouldUpdateConsumer()
        {
            // Arrange
            var consumer = CreateValidConsumer();
            var newName = "Updated Consumer";
            var newDocumentId = "98765432100";
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
                newDocumentId,
                newPhotoUrl,
                newPhoneNumber,
                newEmail,
                newCurrencyId,
                newPhoneCountryCodeId,
                newPreferredLanguageId,
                newTimezoneId);

            // Assert
            Assert.Equal(newName, consumer.Name);
            Assert.Equal(newDocumentId, consumer.DocumentId);
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
        public void Update_WithNullName_ShouldThrowArgumentNullException(string? invalidName)
        {
            // Arrange
            var consumer = CreateValidConsumer();
            var validDocumentId = "98765432100";
            var validPhotoUrl = "https://example.com/new-photo.jpg";
            var validPhoneNumber = "11988888888";
            var validEmail = "updated@test.com";
            var validCurrencyId = Guid.NewGuid();
            var validPhoneCountryCodeId = Guid.NewGuid();
            var validPreferredLanguageId = Guid.NewGuid();
            var validTimezoneId = Guid.NewGuid();

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => consumer.Update(
                invalidName!,
                validDocumentId,
                validPhotoUrl,
                validPhoneNumber,
                validEmail,
                validCurrencyId,
                validPhoneCountryCodeId,
                validPreferredLanguageId,
                validTimezoneId));

            Assert.Equal("name", exception.ParamName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        public void Update_WithEmptyName_ShouldThrowArgumentException(string invalidName)
        {
            // Arrange
            var consumer = CreateValidConsumer();
            var validDocumentId = "98765432100";
            var validPhotoUrl = "https://example.com/new-photo.jpg";
            var validPhoneNumber = "11988888888";
            var validEmail = "updated@test.com";
            var validCurrencyId = Guid.NewGuid();
            var validPhoneCountryCodeId = Guid.NewGuid();
            var validPreferredLanguageId = Guid.NewGuid();
            var validTimezoneId = Guid.NewGuid();

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => consumer.Update(
                invalidName,
                validDocumentId,
                validPhotoUrl,
                validPhoneNumber,
                validEmail,
                validCurrencyId,
                validPhoneCountryCodeId,
                validPreferredLanguageId,
                validTimezoneId));

            Assert.Equal("name", exception.ParamName);
        }

        [Theory]
        [InlineData(null)]
        public void Update_WithNullDocumentId_ShouldThrowArgumentNullException(string? invalidDocumentId)
        {
            // Arrange
            var consumer = CreateValidConsumer();
            var validName = "Updated Consumer";
            var validPhotoUrl = "https://example.com/new-photo.jpg";
            var validPhoneNumber = "11988888888";
            var validEmail = "updated@test.com";
            var validCurrencyId = Guid.NewGuid();
            var validPhoneCountryCodeId = Guid.NewGuid();
            var validPreferredLanguageId = Guid.NewGuid();
            var validTimezoneId = Guid.NewGuid();

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => consumer.Update(
                validName,
                invalidDocumentId!,
                validPhotoUrl,
                validPhoneNumber,
                validEmail,
                validCurrencyId,
                validPhoneCountryCodeId,
                validPreferredLanguageId,
                validTimezoneId));

            Assert.Equal("documentId", exception.ParamName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        public void Update_WithEmptyDocumentId_ShouldThrowArgumentException(string invalidDocumentId)
        {
            // Arrange
            var consumer = CreateValidConsumer();
            var validName = "Updated Consumer";
            var validPhotoUrl = "https://example.com/new-photo.jpg";
            var validPhoneNumber = "11988888888";
            var validEmail = "updated@test.com";
            var validCurrencyId = Guid.NewGuid();
            var validPhoneCountryCodeId = Guid.NewGuid();
            var validPreferredLanguageId = Guid.NewGuid();
            var validTimezoneId = Guid.NewGuid();

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => consumer.Update(
                validName,
                invalidDocumentId,
                validPhotoUrl,
                validPhoneNumber,
                validEmail,
                validCurrencyId,
                validPhoneCountryCodeId,
                validPreferredLanguageId,
                validTimezoneId));

            Assert.Equal("documentId", exception.ParamName);
        }

        [Theory]
        [InlineData(null)]
        public void Update_WithNullPhoneNumber_ShouldThrowArgumentNullException(string? invalidPhoneNumber)
        {
            // Arrange
            var consumer = CreateValidConsumer();
            var validName = "Updated Consumer";
            var validDocumentId = "98765432100";
            var validPhotoUrl = "https://example.com/new-photo.jpg";
            var validEmail = "updated@test.com";
            var validCurrencyId = Guid.NewGuid();
            var validPhoneCountryCodeId = Guid.NewGuid();
            var validPreferredLanguageId = Guid.NewGuid();
            var validTimezoneId = Guid.NewGuid();

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => consumer.Update(
                validName,
                validDocumentId,
                validPhotoUrl,
                invalidPhoneNumber!,
                validEmail,
                validCurrencyId,
                validPhoneCountryCodeId,
                validPreferredLanguageId,
                validTimezoneId));

            Assert.Equal("phoneNumber", exception.ParamName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        [InlineData("abc")]
        [InlineData("123")]
        [InlineData("12345678901234567890123")] // Mais de 20 caracteres
        public void Update_WithInvalidPhoneNumber_ShouldThrowArgumentException(string invalidPhoneNumber)
        {
            // Arrange
            var consumer = CreateValidConsumer();
            var validName = "Updated Consumer";
            var validDocumentId = "98765432100";
            var validPhotoUrl = "https://example.com/new-photo.jpg";
            var validEmail = "updated@test.com";
            var validCurrencyId = Guid.NewGuid();
            var validPhoneCountryCodeId = Guid.NewGuid();
            var validPreferredLanguageId = Guid.NewGuid();
            var validTimezoneId = Guid.NewGuid();

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => consumer.Update(
                validName,
                validDocumentId,
                validPhotoUrl,
                invalidPhoneNumber,
                validEmail,
                validCurrencyId,
                validPhoneCountryCodeId,
                validPreferredLanguageId,
                validTimezoneId));

            Assert.Equal("phoneNumber", exception.ParamName);
        }

        [Theory]
        [InlineData(null)]
        public void Update_WithNullEmail_ShouldThrowArgumentNullException(string? invalidEmail)
        {
            // Arrange
            var consumer = CreateValidConsumer();
            var validName = "Updated Consumer";
            var validDocumentId = "98765432100";
            var validPhotoUrl = "https://example.com/new-photo.jpg";
            var validPhoneNumber = "11988888888";
            var validCurrencyId = Guid.NewGuid();
            var validPhoneCountryCodeId = Guid.NewGuid();
            var validPreferredLanguageId = Guid.NewGuid();
            var validTimezoneId = Guid.NewGuid();

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => consumer.Update(
                validName,
                validDocumentId,
                validPhotoUrl,
                validPhoneNumber,
                invalidEmail!,
                validCurrencyId,
                validPhoneCountryCodeId,
                validPreferredLanguageId,
                validTimezoneId));

            Assert.Equal("email", exception.ParamName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        [InlineData("invalid-email")]
        [InlineData("test@")]
        [InlineData("@test.com")]
        public void Update_WithInvalidEmail_ShouldThrowArgumentException(string invalidEmail)
        {
            // Arrange
            var consumer = CreateValidConsumer();
            var validName = "Updated Consumer";
            var validDocumentId = "98765432100";
            var validPhotoUrl = "https://example.com/new-photo.jpg";
            var validPhoneNumber = "11988888888";
            var validCurrencyId = Guid.NewGuid();
            var validPhoneCountryCodeId = Guid.NewGuid();
            var validPreferredLanguageId = Guid.NewGuid();
            var validTimezoneId = Guid.NewGuid();

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => consumer.Update(
                validName,
                validDocumentId,
                validPhotoUrl,
                validPhoneNumber,
                invalidEmail,
                validCurrencyId,
                validPhoneCountryCodeId,
                validPreferredLanguageId,
                validTimezoneId));

            Assert.Equal("email", exception.ParamName);
        }

        [Fact]
        public void Update_WithEmptyGuidIds_ShouldThrowException()
        {
            // Arrange
            var consumer = CreateValidConsumer();
            var validName = "Updated Consumer";
            var validDocumentId = "98765432100";
            var validPhotoUrl = "https://example.com/new-photo.jpg";
            var validPhoneNumber = "11988888888";
            var validEmail = "updated@test.com";

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => consumer.Update(
                validName,
                validDocumentId,
                validPhotoUrl,
                validPhoneNumber,
                validEmail,
                Guid.Empty,
                Guid.Empty,
                Guid.Empty,
                Guid.Empty));

            Assert.Equal("currencyId", exception.ParamName);
        }

        [Fact]
        public void Addresses_ShouldBeEmptyCollectionByDefault()
        {
            // Arrange & Act
            var consumer = CreateValidConsumer();

            // Assert
            Assert.NotNull(consumer.Addresses);
            Assert.Empty(consumer.Addresses);
            Assert.IsType<List<AddressEntity>>(consumer.Addresses);
        }

        [Fact]
        public void NavigationProperties_ShouldBeNullByDefault()
        {
            // Arrange & Act
            var consumer = CreateValidConsumer();

            // Assert
            Assert.Null(consumer.Currency);
            Assert.Null(consumer.PhoneCountryCode);
            Assert.Null(consumer.PreferredLanguage);
            Assert.Null(consumer.Timezone);
        }

        private ConsumerEntity CreateValidConsumer()
        {
            return new ConsumerEntity(
                "Test Consumer",
                "12345678900",
                "https://example.com/photo.jpg",
                "11999999999",
                "test@test.com",
                _validCurrencyId,
                _validPhoneCountryCodeId,
                _validPreferredLanguageId,
                _validTimezoneId);
        }
    }
} 