using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Language.Flow;
using MSConsumers.Application.Commands.Address;
using MSConsumers.Domain.Entities;
using MSConsumers.Domain.Exceptions;
using MSConsumers.Domain.Interfaces;
using Xunit;

namespace MSConsumers.UnitTests.Handlers.Address;

public class GetAddressesByConsumerIdCommandHandlerTests : AddressHandlerTestsBase
{
    private readonly GetAddressesByConsumerIdCommandHandler _handler;

    public GetAddressesByConsumerIdCommandHandlerTests()
    {
        _handler = new GetAddressesByConsumerIdCommandHandler(_addressRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WhenConsumerHasAddresses_ShouldReturnAddresses()
    {
        // Arrange
        var consumerId = Guid.NewGuid();
        var addresses = new List<AddressEntity>
        {
            CreateValidAddress(consumerId),
            CreateValidAddress(consumerId)
        };

        var command = new GetAddressesByConsumerIdCommand { ConsumerId = consumerId };

        _addressRepositoryMock
            .Setup(x => x.GetByConsumerIdAsync(consumerId))
            .ReturnsAsync(addresses);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.True(result.All(a => a.ConsumerId == consumerId));
    }

    [Fact]
    public async Task Handle_WhenConsumerHasNoAddresses_ShouldReturnEmptyList()
    {
        // Arrange
        var consumerId = Guid.NewGuid();
        var command = new GetAddressesByConsumerIdCommand { ConsumerId = consumerId };

        _addressRepositoryMock
            .Setup(x => x.GetByConsumerIdAsync(consumerId))
            .ReturnsAsync(new List<AddressEntity>());

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
} 