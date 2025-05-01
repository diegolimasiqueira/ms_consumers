using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Language.Flow;
using MsConsumers.Application.Commands.Consumer;
using MsConsumers.Domain.Entities;
using MsConsumers.Domain.Interfaces;
using Xunit;

namespace MsConsumers.UnitTests.Handlers.Consumer;

public class CreateConsumerCommandHandlerTests
{
    private readonly Mock<IConsumerRepository> _consumerRepositoryMock;
    private readonly CreateConsumerCommandHandler _handler;

    public CreateConsumerCommandHandlerTests()
    {
        _consumerRepositoryMock = new Mock<IConsumerRepository>();
        _handler = new CreateConsumerCommandHandler(_consumerRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidCommand_ShouldCreateConsumer()
    {
        // Arrange
        var command = new CreateConsumerCommand
        {
            Name = "Test Consumer",
            DocumentId = "12345678900",
            PhotoUrl = "https://example.com/photo.jpg",
            PhoneNumber = "11999999999",
            Email = "test@test.com",
            CurrencyId = Guid.NewGuid(),
            PhoneCountryCodeId = Guid.NewGuid(),
            PreferredLanguageId = Guid.NewGuid(),
            TimezoneId = Guid.NewGuid()
        };

        _consumerRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<ConsumerEntity>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Id);
        Assert.Equal(command.Name, result.Name);
        Assert.Equal(command.DocumentId, result.DocumentId);
        Assert.Equal(command.PhotoUrl, result.PhotoUrl);
        Assert.Equal(command.PhoneNumber, result.PhoneNumber);
        Assert.Equal(command.Email, result.Email);
        Assert.Equal(command.CurrencyId, result.CurrencyId);
        Assert.Equal(command.PhoneCountryCodeId, result.PhoneCountryCodeId);
        Assert.Equal(command.PreferredLanguageId, result.PreferredLanguageId);
        Assert.Equal(command.TimezoneId, result.TimezoneId);
        Assert.NotEqual(default, result.CreatedAt);
        Assert.NotEqual(default, result.UpdatedAt);

        _consumerRepositoryMock.Verify(
            x => x.AddAsync(It.Is<ConsumerEntity>(c =>
                c.Name == command.Name &&
                c.DocumentId == command.DocumentId &&
                c.PhotoUrl == command.PhotoUrl &&
                c.PhoneNumber == command.PhoneNumber &&
                c.Email == command.Email &&
                c.CurrencyId == command.CurrencyId &&
                c.PhoneCountryCodeId == command.PhoneCountryCodeId &&
                c.PreferredLanguageId == command.PreferredLanguageId &&
                c.TimezoneId == command.TimezoneId)),
            Times.Once);
    }

    [Fact]
    public async Task Handle_WithNullName_ShouldThrowException()
    {
        // Arrange
        var command = new CreateConsumerCommand
        {
            Name = "",
            DocumentId = "12345678900",
            PhotoUrl = "https://example.com/photo.jpg",
            PhoneNumber = "11999999999",
            Email = "test@test.com",
            CurrencyId = Guid.NewGuid(),
            PhoneCountryCodeId = Guid.NewGuid(),
            PreferredLanguageId = Guid.NewGuid(),
            TimezoneId = Guid.NewGuid()
        };

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WithEmptyName_ShouldThrowException()
    {
        // Arrange
        var command = new CreateConsumerCommand
        {
            Name = "",
            DocumentId = "12345678900",
            PhotoUrl = "https://example.com/photo.jpg",
            PhoneNumber = "11999999999",
            Email = "test@test.com",
            CurrencyId = Guid.NewGuid(),
            PhoneCountryCodeId = Guid.NewGuid(),
            PreferredLanguageId = Guid.NewGuid(),
            TimezoneId = Guid.NewGuid()
        };

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WithNullDocumentId_ShouldThrowException()
    {
        // Arrange
        var command = new CreateConsumerCommand
        {
            Name = "Test Consumer",
            DocumentId = "",
            PhotoUrl = "https://example.com/photo.jpg",
            PhoneNumber = "11999999999",
            Email = "test@test.com",
            CurrencyId = Guid.NewGuid(),
            PhoneCountryCodeId = Guid.NewGuid(),
            PreferredLanguageId = Guid.NewGuid(),
            TimezoneId = Guid.NewGuid()
        };

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WithEmptyDocumentId_ShouldThrowException()
    {
        // Arrange
        var command = new CreateConsumerCommand
        {
            Name = "Test Consumer",
            DocumentId = "",
            PhotoUrl = "https://example.com/photo.jpg",
            PhoneNumber = "11999999999",
            Email = "test@test.com",
            CurrencyId = Guid.NewGuid(),
            PhoneCountryCodeId = Guid.NewGuid(),
            PreferredLanguageId = Guid.NewGuid(),
            TimezoneId = Guid.NewGuid()
        };

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WithNullEmail_ShouldThrowException()
    {
        // Arrange
        var command = new CreateConsumerCommand
        {
            Name = "Test Consumer",
            DocumentId = "12345678900",
            PhotoUrl = "https://example.com/photo.jpg",
            PhoneNumber = "11999999999",
            Email = "",
            CurrencyId = Guid.NewGuid(),
            PhoneCountryCodeId = Guid.NewGuid(),
            PreferredLanguageId = Guid.NewGuid(),
            TimezoneId = Guid.NewGuid()
        };

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WithEmptyEmail_ShouldThrowException()
    {
        // Arrange
        var command = new CreateConsumerCommand
        {
            Name = "Test Consumer",
            DocumentId = "12345678900",
            PhotoUrl = "https://example.com/photo.jpg",
            PhoneNumber = "11999999999",
            Email = "",
            CurrencyId = Guid.NewGuid(),
            PhoneCountryCodeId = Guid.NewGuid(),
            PreferredLanguageId = Guid.NewGuid(),
            TimezoneId = Guid.NewGuid()
        };

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WithInvalidEmail_ShouldThrowException()
    {
        // Arrange
        var command = new CreateConsumerCommand
        {
            Name = "Test Consumer",
            DocumentId = "12345678900",
            PhotoUrl = "https://example.com/photo.jpg",
            PhoneNumber = "11999999999",
            Email = "invalid-email",
            CurrencyId = Guid.NewGuid(),
            PhoneCountryCodeId = Guid.NewGuid(),
            PreferredLanguageId = Guid.NewGuid(),
            TimezoneId = Guid.NewGuid()
        };

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WithNullPhoneNumber_ShouldThrowException()
    {
        // Arrange
        var command = new CreateConsumerCommand
        {
            Name = "Test Consumer",
            DocumentId = "12345678900",
            PhotoUrl = "https://example.com/photo.jpg",
            PhoneNumber = "",
            Email = "test@test.com",
            CurrencyId = Guid.NewGuid(),
            PhoneCountryCodeId = Guid.NewGuid(),
            PreferredLanguageId = Guid.NewGuid(),
            TimezoneId = Guid.NewGuid()
        };

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WithEmptyPhoneNumber_ShouldThrowException()
    {
        // Arrange
        var command = new CreateConsumerCommand
        {
            Name = "Test Consumer",
            DocumentId = "12345678900",
            PhotoUrl = "https://example.com/photo.jpg",
            PhoneNumber = "",
            Email = "test@test.com",
            CurrencyId = Guid.NewGuid(),
            PhoneCountryCodeId = Guid.NewGuid(),
            PreferredLanguageId = Guid.NewGuid(),
            TimezoneId = Guid.NewGuid()
        };

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WithInvalidPhoneNumber_ShouldThrowException()
    {
        // Arrange
        var command = new CreateConsumerCommand
        {
            Name = "Test Consumer",
            DocumentId = "12345678900",
            PhotoUrl = "https://example.com/photo.jpg",
            PhoneNumber = "123",
            Email = "test@test.com",
            CurrencyId = Guid.NewGuid(),
            PhoneCountryCodeId = Guid.NewGuid(),
            PreferredLanguageId = Guid.NewGuid(),
            TimezoneId = Guid.NewGuid()
        };

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }
} 