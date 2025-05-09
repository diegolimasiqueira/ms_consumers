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

public class UpdateAddressCommandHandlerTests : AddressHandlerTestsBase
{
    private readonly UpdateAddressCommandHandler _handler;

    public UpdateAddressCommandHandlerTests()
    {
        _handler = new UpdateAddressCommandHandler(_addressRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WhenAddressExists_ShouldUpdateAddress()
    {
        // Arrange
        var address = CreateValidAddress();
        var command = new UpdateAddressCommand
        {
            Id = address.Id,
            StreetAddress = "Rua Nova, 456",
            City = "Rio de Janeiro",
            State = "RJ",
            PostalCode = "98765-432",
            Latitude = 22.9068,
            Longitude = 43.1729,
            IsDefault = false,
            CountryId = Guid.NewGuid()
        };

        _addressRepositoryMock
            .Setup(x => x.GetByIdAsync(address.Id))
            .ReturnsAsync(address);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(command.Id, result.Id);
        Assert.Equal(command.StreetAddress, result.StreetAddress);
        Assert.Equal(command.City, result.City);
        Assert.Equal(command.State, result.State);
        Assert.Equal(command.PostalCode, result.PostalCode);
        Assert.Equal(command.Latitude, result.Latitude);
        Assert.Equal(command.Longitude, result.Longitude);
        Assert.Equal(command.IsDefault, result.IsDefault);
        Assert.Equal(command.CountryId, result.CountryId);

        _addressRepositoryMock.Verify(
            x => x.UpdateAsync(It.Is<AddressEntity>(a => 
                a.Id == command.Id &&
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
    public async Task Handle_WhenAddressDoesNotExist_ShouldThrowAddressNotFoundException()
    {
        // Arrange
        var command = new UpdateAddressCommand
        {
            Id = Guid.NewGuid(),
            StreetAddress = "Rua Nova, 456",
            City = "Rio de Janeiro",
            State = "RJ",
            PostalCode = "98765-432",
            Latitude = 22.9068,
            Longitude = 43.1729,
            IsDefault = false,
            CountryId = Guid.NewGuid()
        };

        _addressRepositoryMock
            .Setup(x => x.GetByIdAsync(command.Id))
            .ReturnsAsync((AddressEntity?)null);

        // Act & Assert
        await Assert.ThrowsAsync<AddressNotFoundException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }
} 