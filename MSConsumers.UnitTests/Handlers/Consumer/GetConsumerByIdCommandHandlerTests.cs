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

public class GetConsumerByIdCommandHandlerTests
{
    private readonly Mock<IConsumerRepository> _consumerRepositoryMock;
    private readonly GetConsumerByIdCommandHandler _handler;

    public GetConsumerByIdCommandHandlerTests()
    {
        _consumerRepositoryMock = new Mock<IConsumerRepository>();
        _handler = new GetConsumerByIdCommandHandler(_consumerRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidId_ShouldReturnConsumer()
    {
        // Arrange
        var consumerId = Guid.NewGuid();
        var consumer = new ConsumerEntity(
            "Test Consumer",
            "12345678900",
            "https://example.com/photo.jpg",
            "11999999999",
            "test@test.com",
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid());

        _consumerRepositoryMock
            .Setup(x => x.GetByIdAsync(consumerId))
            .ReturnsAsync(consumer);

        var command = new GetConsumerByIdCommand { Id = consumerId };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(consumer.Id, result.Id);
        Assert.Equal(consumer.Name, result.Name);
        Assert.Equal(consumer.DocumentId, result.DocumentId);
        Assert.Equal(consumer.PhotoUrl, result.PhotoUrl);
        Assert.Equal(consumer.PhoneNumber, result.PhoneNumber);
        Assert.Equal(consumer.Email, result.Email);
        Assert.Equal(consumer.CurrencyId, result.CurrencyId);
        Assert.Equal(consumer.PhoneCountryCodeId, result.PhoneCountryCodeId);
        Assert.Equal(consumer.PreferredLanguageId, result.PreferredLanguageId);
        Assert.Equal(consumer.TimezoneId, result.TimezoneId);
        Assert.Equal(consumer.CreatedAt, result.CreatedAt);
        Assert.Equal(consumer.UpdatedAt, result.UpdatedAt);

        _consumerRepositoryMock.Verify(x => x.GetByIdAsync(consumerId), Times.Once);
    }

    [Fact]
    public async Task Handle_WithNonExistentId_ShouldThrowConsumerNotFoundException()
    {
        // Arrange
        var consumerId = Guid.NewGuid();

        _consumerRepositoryMock
            .Setup(x => x.GetByIdAsync(consumerId))
            .ReturnsAsync((ConsumerEntity)null);

        var command = new GetConsumerByIdCommand { Id = consumerId };

        // Act & Assert
        await Assert.ThrowsAsync<ConsumerNotFoundException>(() =>
            _handler.Handle(command, CancellationToken.None));

        _consumerRepositoryMock.Verify(x => x.GetByIdAsync(consumerId), Times.Once);
    }

    [Fact]
    public async Task Handle_WithEmptyId_ShouldThrowArgumentException()
    {
        // Arrange
        var command = new GetConsumerByIdCommand { Id = Guid.Empty };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() =>
            _handler.Handle(command, CancellationToken.None));

        _consumerRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
    }
} 