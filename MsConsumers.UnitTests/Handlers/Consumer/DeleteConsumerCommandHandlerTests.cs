using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using MsConsumers.Application.Commands.Consumer;
using MsConsumers.Domain.Entities;
using MsConsumers.Domain.Exceptions;
using MsConsumers.Domain.Interfaces;
using Xunit;

namespace MsConsumers.UnitTests.Handlers.Consumer;

public class DeleteConsumerCommandHandlerTests
{
    private readonly Mock<IConsumerRepository> _consumerRepositoryMock;
    private readonly DeleteConsumerCommandHandler _handler;

    public DeleteConsumerCommandHandlerTests()
    {
        _consumerRepositoryMock = new Mock<IConsumerRepository>();
        _handler = new DeleteConsumerCommandHandler(_consumerRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidId_ShouldDeleteConsumer()
    {
        // Arrange
        var consumerId = Guid.NewGuid();
        var existingConsumer = new ConsumerEntity(
            "Test Name",
            "12345678900",
            "https://example.com/photo.jpg",
            "11999999999",
            "test@test.com",
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid());

        var command = new DeleteConsumerCommand { Id = consumerId };

        _consumerRepositoryMock
            .Setup(x => x.GetByIdAsync(consumerId))
            .ReturnsAsync(existingConsumer);

        _consumerRepositoryMock
            .Setup(x => x.DeleteAsync(consumerId))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(Unit.Value, result);
        _consumerRepositoryMock.Verify(x => x.GetByIdAsync(consumerId), Times.Once);
        _consumerRepositoryMock.Verify(x => x.DeleteAsync(consumerId), Times.Once);
    }

    [Fact]
    public async Task Handle_WithNonExistentId_ShouldThrowConsumerNotFoundException()
    {
        // Arrange
        var consumerId = Guid.NewGuid();
        var command = new DeleteConsumerCommand { Id = consumerId };

        _consumerRepositoryMock
            .Setup(x => x.GetByIdAsync(consumerId))
            .ReturnsAsync((ConsumerEntity?)null);

        // Act & Assert
        await Assert.ThrowsAsync<ConsumerNotFoundException>(() =>
            _handler.Handle(command, CancellationToken.None));

        _consumerRepositoryMock.Verify(x => x.GetByIdAsync(consumerId), Times.Once);
        _consumerRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<Guid>()), Times.Never);
    }
} 