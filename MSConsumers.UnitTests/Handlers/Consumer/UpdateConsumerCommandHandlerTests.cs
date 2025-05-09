using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using MSConsumers.Application.Commands.Consumer;
using MSConsumers.Domain.Entities;
using MSConsumers.Domain.Exceptions;
using MSConsumers.Domain.Interfaces;
using Xunit;

namespace MSConsumers.UnitTests.Handlers.Consumer;

public class UpdateConsumerCommandHandlerTests
{
    private readonly Mock<IConsumerRepository> _consumerRepositoryMock;
    private readonly UpdateConsumerCommandHandler _handler;

    public UpdateConsumerCommandHandlerTests()
    {
        _consumerRepositoryMock = new Mock<IConsumerRepository>();
        _handler = new UpdateConsumerCommandHandler(_consumerRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidCommand_ShouldUpdateConsumer()
    {
        // Arrange
        var consumerId = Guid.NewGuid();
        var existingConsumer = new ConsumerEntity(
            "Old Name",
            "12345678900",
            "https://example.com/old-photo.jpg",
            "11999999999",
            "old@test.com",
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid());

        var command = new UpdateConsumerCommand
        {
            Id = existingConsumer.Id,
            Name = "New Name",
            DocumentId = "98765432100",
            PhotoUrl = "https://example.com/new-photo.jpg",
            PhoneNumber = "11988888888",
            Email = "new@test.com",
            CurrencyId = Guid.NewGuid(),
            PhoneCountryCodeId = Guid.NewGuid(),
            PreferredLanguageId = Guid.NewGuid(),
            TimezoneId = Guid.NewGuid()
        };

        _consumerRepositoryMock
            .Setup(x => x.GetByIdAsync(existingConsumer.Id))
            .ReturnsAsync(existingConsumer);

        _consumerRepositoryMock
            .Setup(x => x.UpdateAsync(It.IsAny<ConsumerEntity>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingConsumer.Id, result.Id);
        Assert.Equal(command.Name, result.Name);
        Assert.Equal(command.DocumentId, result.DocumentId);
        Assert.Equal(command.PhotoUrl, result.PhotoUrl);
        Assert.Equal(command.PhoneNumber, result.PhoneNumber);
        Assert.Equal(command.Email, result.Email);
        Assert.Equal(command.CurrencyId, result.CurrencyId);
        Assert.Equal(command.PhoneCountryCodeId, result.PhoneCountryCodeId);
        Assert.Equal(command.PreferredLanguageId, result.PreferredLanguageId);
        Assert.Equal(command.TimezoneId, result.TimezoneId);

        _consumerRepositoryMock.Verify(x => x.GetByIdAsync(existingConsumer.Id), Times.Once);
        _consumerRepositoryMock.Verify(x => x.UpdateAsync(It.Is<ConsumerEntity>(c =>
            c.Id == existingConsumer.Id &&
            c.Name == command.Name &&
            c.DocumentId == command.DocumentId &&
            c.PhotoUrl == command.PhotoUrl &&
            c.PhoneNumber == command.PhoneNumber &&
            c.Email == command.Email &&
            c.CurrencyId == command.CurrencyId &&
            c.PhoneCountryCodeId == command.PhoneCountryCodeId &&
            c.PreferredLanguageId == command.PreferredLanguageId &&
            c.TimezoneId == command.TimezoneId)), Times.Once);
    }

    [Fact]
    public async Task Handle_WithNonExistentId_ShouldThrowConsumerNotFoundException()
    {
        // Arrange
        var consumerId = Guid.NewGuid();
        var command = new UpdateConsumerCommand
        {
            Id = consumerId,
            Name = "New Name",
            DocumentId = "98765432100",
            PhotoUrl = "https://example.com/new-photo.jpg",
            PhoneNumber = "11988888888",
            Email = "new@test.com",
            CurrencyId = Guid.NewGuid(),
            PhoneCountryCodeId = Guid.NewGuid(),
            PreferredLanguageId = Guid.NewGuid(),
            TimezoneId = Guid.NewGuid()
        };

        _consumerRepositoryMock
            .Setup(x => x.GetByIdAsync(consumerId))
            .ReturnsAsync((ConsumerEntity)null);

        // Act & Assert
        await Assert.ThrowsAsync<ConsumerNotFoundException>(() =>
            _handler.Handle(command, CancellationToken.None));

        _consumerRepositoryMock.Verify(x => x.GetByIdAsync(consumerId), Times.Once);
        _consumerRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<ConsumerEntity>()), Times.Never);
    }

    [Fact]
    public async Task Handle_WithEmptyId_ShouldThrowArgumentException()
    {
        // Arrange
        var command = new UpdateConsumerCommand
        {
            Id = Guid.Empty,
            Name = "New Name",
            DocumentId = "98765432100",
            PhotoUrl = "https://example.com/new-photo.jpg",
            PhoneNumber = "11988888888",
            Email = "new@test.com",
            CurrencyId = Guid.NewGuid(),
            PhoneCountryCodeId = Guid.NewGuid(),
            PreferredLanguageId = Guid.NewGuid(),
            TimezoneId = Guid.NewGuid()
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() =>
            _handler.Handle(command, CancellationToken.None));

        _consumerRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
        _consumerRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<ConsumerEntity>()), Times.Never);
    }

    [Theory]
    [InlineData(null)]
    public async Task Handle_WithNullName_ShouldThrowArgumentNullException(string invalidName)
    {
        // Arrange
        var consumerId = Guid.NewGuid();
        var existingConsumer = new ConsumerEntity(
            "Old Name",
            "12345678900",
            "https://example.com/old-photo.jpg",
            "11999999999",
            "old@test.com",
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid());

        var command = new UpdateConsumerCommand
        {
            Id = consumerId,
            Name = invalidName,
            DocumentId = "98765432100",
            PhotoUrl = "https://example.com/new-photo.jpg",
            PhoneNumber = "11988888888",
            Email = "new@test.com",
            CurrencyId = Guid.NewGuid(),
            PhoneCountryCodeId = Guid.NewGuid(),
            PreferredLanguageId = Guid.NewGuid(),
            TimezoneId = Guid.NewGuid()
        };

        _consumerRepositoryMock
            .Setup(x => x.GetByIdAsync(consumerId))
            .ReturnsAsync(existingConsumer);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _handler.Handle(command, CancellationToken.None));
        Assert.Equal("name", exception.ParamName);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public async Task Handle_WithEmptyName_ShouldThrowArgumentException(string invalidName)
    {
        // Arrange
        var consumerId = Guid.NewGuid();
        var existingConsumer = new ConsumerEntity(
            "Old Name",
            "12345678900",
            "https://example.com/old-photo.jpg",
            "11999999999",
            "old@test.com",
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid());

        var command = new UpdateConsumerCommand
        {
            Id = consumerId,
            Name = invalidName,
            DocumentId = "98765432100",
            PhotoUrl = "https://example.com/new-photo.jpg",
            PhoneNumber = "11988888888",
            Email = "new@test.com",
            CurrencyId = Guid.NewGuid(),
            PhoneCountryCodeId = Guid.NewGuid(),
            PreferredLanguageId = Guid.NewGuid(),
            TimezoneId = Guid.NewGuid()
        };

        _consumerRepositoryMock
            .Setup(x => x.GetByIdAsync(consumerId))
            .ReturnsAsync(existingConsumer);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            _handler.Handle(command, CancellationToken.None));
        Assert.Equal("name", exception.ParamName);
    }

    [Theory]
    [InlineData(null)]
    public async Task Handle_WithNullDocumentId_ShouldThrowArgumentNullException(string invalidDocumentId)
    {
        // Arrange
        var consumerId = Guid.NewGuid();
        var existingConsumer = new ConsumerEntity(
            "New Name",
            "12345678900",
            "https://example.com/old-photo.jpg",
            "11999999999",
            "old@test.com",
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid());

        var command = new UpdateConsumerCommand
        {
            Id = consumerId,
            Name = "New Name",
            DocumentId = invalidDocumentId,
            PhotoUrl = "https://example.com/new-photo.jpg",
            PhoneNumber = "11988888888",
            Email = "new@test.com",
            CurrencyId = Guid.NewGuid(),
            PhoneCountryCodeId = Guid.NewGuid(),
            PreferredLanguageId = Guid.NewGuid(),
            TimezoneId = Guid.NewGuid()
        };

        _consumerRepositoryMock
            .Setup(x => x.GetByIdAsync(consumerId))
            .ReturnsAsync(existingConsumer);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _handler.Handle(command, CancellationToken.None));
        Assert.Equal("documentId", exception.ParamName);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public async Task Handle_WithEmptyDocumentId_ShouldThrowArgumentException(string invalidDocumentId)
    {
        // Arrange
        var consumerId = Guid.NewGuid();
        var existingConsumer = new ConsumerEntity(
            "New Name",
            "12345678900",
            "https://example.com/old-photo.jpg",
            "11999999999",
            "old@test.com",
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid());

        var command = new UpdateConsumerCommand
        {
            Id = consumerId,
            Name = "New Name",
            DocumentId = invalidDocumentId,
            PhotoUrl = "https://example.com/new-photo.jpg",
            PhoneNumber = "11988888888",
            Email = "new@test.com",
            CurrencyId = Guid.NewGuid(),
            PhoneCountryCodeId = Guid.NewGuid(),
            PreferredLanguageId = Guid.NewGuid(),
            TimezoneId = Guid.NewGuid()
        };

        _consumerRepositoryMock
            .Setup(x => x.GetByIdAsync(consumerId))
            .ReturnsAsync(existingConsumer);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            _handler.Handle(command, CancellationToken.None));
        Assert.Equal("documentId", exception.ParamName);
    }

    [Theory]
    [InlineData(null)]
    public async Task Handle_WithNullEmail_ShouldThrowArgumentNullException(string invalidEmail)
    {
        // Arrange
        var consumerId = Guid.NewGuid();
        var existingConsumer = new ConsumerEntity(
            "New Name",
            "12345678900",
            "https://example.com/old-photo.jpg",
            "11999999999",
            "old@test.com",
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid());

        var command = new UpdateConsumerCommand
        {
            Id = consumerId,
            Name = "New Name",
            DocumentId = "98765432100",
            PhotoUrl = "https://example.com/new-photo.jpg",
            PhoneNumber = "11988888888",
            Email = invalidEmail,
            CurrencyId = Guid.NewGuid(),
            PhoneCountryCodeId = Guid.NewGuid(),
            PreferredLanguageId = Guid.NewGuid(),
            TimezoneId = Guid.NewGuid()
        };

        _consumerRepositoryMock
            .Setup(x => x.GetByIdAsync(consumerId))
            .ReturnsAsync(existingConsumer);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _handler.Handle(command, CancellationToken.None));
        Assert.Equal("email", exception.ParamName);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("invalid-email")]
    [InlineData("test@")]
    [InlineData("@test.com")]
    public async Task Handle_WithInvalidEmail_ShouldThrowArgumentException(string invalidEmail)
    {
        // Arrange
        var consumerId = Guid.NewGuid();
        var existingConsumer = new ConsumerEntity(
            "New Name",
            "12345678900",
            "https://example.com/old-photo.jpg",
            "11999999999",
            "old@test.com",
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid());

        var command = new UpdateConsumerCommand
        {
            Id = consumerId,
            Name = "New Name",
            DocumentId = "98765432100",
            PhotoUrl = "https://example.com/new-photo.jpg",
            PhoneNumber = "11988888888",
            Email = invalidEmail,
            CurrencyId = Guid.NewGuid(),
            PhoneCountryCodeId = Guid.NewGuid(),
            PreferredLanguageId = Guid.NewGuid(),
            TimezoneId = Guid.NewGuid()
        };

        _consumerRepositoryMock
            .Setup(x => x.GetByIdAsync(consumerId))
            .ReturnsAsync(existingConsumer);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            _handler.Handle(command, CancellationToken.None));
        Assert.Equal("email", exception.ParamName);
    }

    [Theory]
    [InlineData(null)]
    public async Task Handle_WithNullPhoneNumber_ShouldThrowArgumentNullException(string invalidPhoneNumber)
    {
        // Arrange
        var consumerId = Guid.NewGuid();
        var existingConsumer = new ConsumerEntity(
            "New Name",
            "12345678900",
            "https://example.com/old-photo.jpg",
            "11999999999",
            "old@test.com",
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid());

        var command = new UpdateConsumerCommand
        {
            Id = consumerId,
            Name = "New Name",
            DocumentId = "98765432100",
            PhotoUrl = "https://example.com/new-photo.jpg",
            PhoneNumber = invalidPhoneNumber,
            Email = "new@test.com",
            CurrencyId = Guid.NewGuid(),
            PhoneCountryCodeId = Guid.NewGuid(),
            PreferredLanguageId = Guid.NewGuid(),
            TimezoneId = Guid.NewGuid()
        };

        _consumerRepositoryMock
            .Setup(x => x.GetByIdAsync(consumerId))
            .ReturnsAsync(existingConsumer);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _handler.Handle(command, CancellationToken.None));
        Assert.Equal("phoneNumber", exception.ParamName);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("12345")]
    [InlineData("12345678901234567890123")]
    [InlineData("abc")]
    public async Task Handle_WithInvalidPhoneNumber_ShouldThrowArgumentException(string invalidPhoneNumber)
    {
        // Arrange
        var consumerId = Guid.NewGuid();
        var existingConsumer = new ConsumerEntity(
            "New Name",
            "12345678900",
            "https://example.com/old-photo.jpg",
            "11999999999",
            "old@test.com",
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid());

        var command = new UpdateConsumerCommand
        {
            Id = consumerId,
            Name = "New Name",
            DocumentId = "98765432100",
            PhotoUrl = "https://example.com/new-photo.jpg",
            PhoneNumber = invalidPhoneNumber,
            Email = "new@test.com",
            CurrencyId = Guid.NewGuid(),
            PhoneCountryCodeId = Guid.NewGuid(),
            PreferredLanguageId = Guid.NewGuid(),
            TimezoneId = Guid.NewGuid()
        };

        _consumerRepositoryMock
            .Setup(x => x.GetByIdAsync(consumerId))
            .ReturnsAsync(existingConsumer);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            _handler.Handle(command, CancellationToken.None));
        Assert.Equal("phoneNumber", exception.ParamName);
    }

    [Fact]
    public async Task Handle_WithEmptyGuidIds_ShouldThrowArgumentException()
    {
        // Arrange
        var consumerId = Guid.NewGuid();
        var existingConsumer = new ConsumerEntity(
            "New Name",
            "12345678900",
            "https://example.com/old-photo.jpg",
            "11999999999",
            "old@test.com",
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid());

        var command = new UpdateConsumerCommand
        {
            Id = consumerId,
            Name = "New Name",
            DocumentId = "98765432100",
            PhotoUrl = "https://example.com/new-photo.jpg",
            PhoneNumber = "11988888888",
            Email = "new@test.com",
            CurrencyId = Guid.Empty,
            PhoneCountryCodeId = Guid.Empty,
            PreferredLanguageId = Guid.Empty,
            TimezoneId = Guid.Empty
        };

        _consumerRepositoryMock
            .Setup(x => x.GetByIdAsync(consumerId))
            .ReturnsAsync(existingConsumer);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() =>
            _handler.Handle(command, CancellationToken.None));

        _consumerRepositoryMock.Verify(x => x.GetByIdAsync(consumerId), Times.Once);
        _consumerRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<ConsumerEntity>()), Times.Never);
    }
} 