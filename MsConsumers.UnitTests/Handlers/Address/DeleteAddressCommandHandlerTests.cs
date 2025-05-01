using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Language.Flow;
using MsConsumers.Application.Commands.Address;
using MsConsumers.Domain.Entities;
using MsConsumers.Domain.Exceptions;
using MsConsumers.Domain.Interfaces;
using Xunit;

namespace MsConsumers.UnitTests.Handlers.Address;

public class DeleteAddressCommandHandlerTests : AddressHandlerTestsBase
{
    private readonly DeleteAddressCommandHandler _handler;

    public DeleteAddressCommandHandlerTests()
    {
        _handler = new DeleteAddressCommandHandler(_addressRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WhenAddressExists_ShouldDeleteAddress()
    {
        // Arrange
        var address = CreateValidAddress();
        var command = new DeleteAddressCommand { Id = address.Id };

        _addressRepositoryMock
            .Setup(x => x.GetByIdAsync(address.Id))
            .ReturnsAsync(address);

        _addressRepositoryMock
            .Setup(x => x.DeleteAsync(It.IsAny<AddressEntity>()))
            .Returns(Task.CompletedTask);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _addressRepositoryMock.Verify(
            x => x.DeleteAsync(It.Is<AddressEntity>(a => a.Id == address.Id)),
            Times.Once);
    }

    [Fact]
    public async Task Handle_WhenAddressDoesNotExist_ShouldThrowAddressNotFoundException()
    {
        // Arrange
        var command = new DeleteAddressCommand { Id = Guid.NewGuid() };

        _addressRepositoryMock
            .Setup(x => x.GetByIdAsync(command.Id))
            .ReturnsAsync((AddressEntity?)null);

        // Act & Assert
        await Assert.ThrowsAsync<AddressNotFoundException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }
} 