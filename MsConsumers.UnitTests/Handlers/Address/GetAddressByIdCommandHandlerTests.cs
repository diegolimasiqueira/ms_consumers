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

public class GetAddressByIdCommandHandlerTests : AddressHandlerTestsBase
{
    private readonly GetAddressByIdCommandHandler _handler;

    public GetAddressByIdCommandHandlerTests()
    {
        _handler = new GetAddressByIdCommandHandler(_addressRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WhenAddressExists_ShouldReturnAddress()
    {
        // Arrange
        var address = CreateValidAddress();
        var command = new GetAddressByIdCommand { Id = address.Id };

        _addressRepositoryMock
            .Setup(x => x.GetByIdAsync(address.Id))
            .ReturnsAsync(address);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(address.Id, result.Id);
        Assert.Equal(address.ConsumerId, result.ConsumerId);
        Assert.Equal(address.StreetAddress, result.StreetAddress);
        Assert.Equal(address.City, result.City);
        Assert.Equal(address.State, result.State);
        Assert.Equal(address.PostalCode, result.PostalCode);
        Assert.Equal(address.Latitude, result.Latitude);
        Assert.Equal(address.Longitude, result.Longitude);
        Assert.Equal(address.IsDefault, result.IsDefault);
        Assert.Equal(address.CountryId, result.CountryId);
    }

    [Fact]
    public async Task Handle_WhenAddressDoesNotExist_ShouldThrowAddressNotFoundException()
    {
        // Arrange
        var command = new GetAddressByIdCommand { Id = Guid.NewGuid() };

        _addressRepositoryMock
            .Setup(x => x.GetByIdAsync(command.Id))
            .ReturnsAsync((AddressEntity?)null);

        // Act & Assert
        await Assert.ThrowsAsync<AddressNotFoundException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }
} 