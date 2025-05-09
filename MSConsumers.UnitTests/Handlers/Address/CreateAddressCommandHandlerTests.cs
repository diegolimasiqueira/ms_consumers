using System;
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

public class CreateAddressCommandHandlerTests : AddressHandlerTestsBase
{
    private readonly CreateAddressCommandHandler _handler;

    public CreateAddressCommandHandlerTests()
    {
        _handler = new CreateAddressCommandHandler(
            _addressRepositoryMock.Object,
            _consumerRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WhenConsumerExists_ShouldCreateAddress()
    {
        // Arrange
        var consumer = CreateValidConsumer();
        var command = new CreateAddressCommand
        {
            ConsumerId = consumer.Id,
            StreetAddress = "Rua Teste, 123",
            City = "São Paulo",
            State = "SP",
            PostalCode = "12345-678",
            Latitude = 23.5505,
            Longitude = 46.6333,
            IsDefault = true,
            CountryId = Guid.NewGuid()
        };

        _consumerRepositoryMock
            .Setup(x => x.GetByIdAsync(consumer.Id))
            .ReturnsAsync(consumer);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Id);
        Assert.Equal(command.ConsumerId, result.ConsumerId);
        Assert.Equal(command.StreetAddress, result.StreetAddress);
        Assert.Equal(command.City, result.City);
        Assert.Equal(command.State, result.State);
        Assert.Equal(command.PostalCode, result.PostalCode);
        Assert.Equal(command.Latitude, result.Latitude);
        Assert.Equal(command.Longitude, result.Longitude);
        Assert.Equal(command.IsDefault, result.IsDefault);
        Assert.Equal(command.CountryId, result.CountryId);

        _addressRepositoryMock.Verify(
            x => x.AddAsync(It.Is<AddressEntity>(a => 
                a.ConsumerId == command.ConsumerId &&
                a.StreetAddress == command.StreetAddress &&
                a.City == command.City &&
                a.State == command.State &&
                a.PostalCode == command.PostalCode &&
                a.Latitude == command.Latitude &&
                a.Longitude == command.Longitude &&
                a.IsDefault == command.IsDefault &&
                a.CountryId == command.CountryId)),
            Times.Once);
    }

    [Fact]
    public async Task Handle_WhenConsumerDoesNotExist_ShouldThrowConsumerNotFoundException()
    {
        // Arrange
        var command = new CreateAddressCommand
        {
            ConsumerId = Guid.NewGuid(),
            StreetAddress = "Rua Teste, 123",
            City = "São Paulo",
            State = "SP",
            PostalCode = "12345-678",
            Latitude = 23.5505,
            Longitude = 46.6333,
            IsDefault = true,
            CountryId = Guid.NewGuid()
        };

        _consumerRepositoryMock
            .Setup(x => x.GetByIdAsync(command.ConsumerId))
            .ReturnsAsync((ConsumerEntity?)null);

        // Act & Assert
        await Assert.ThrowsAsync<ConsumerNotFoundException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }
} 