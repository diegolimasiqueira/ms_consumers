using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MSConsumers.Domain.Entities;
using MSConsumers.Domain.Exceptions;
using MSConsumers.Domain.Interfaces;

namespace MSConsumers.Application.Commands.Address;

/// <summary>
/// Handler for the GetAddressByIdCommand
/// </summary>
public class GetAddressByIdCommandHandler : IRequestHandler<GetAddressByIdCommand, GetAddressByIdCommandResponse?>
{
    private readonly IAddressRepository _addressRepository;

    /// <summary>
    /// Initializes a new instance of the GetAddressByIdCommandHandler
    /// </summary>
    /// <param name="addressRepository">The address repository</param>
    public GetAddressByIdCommandHandler(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
    }

    /// <summary>
    /// Handles the GetAddressByIdCommand
    /// </summary>
    /// <param name="request">The command request</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The address response or null if not found</returns>
    public async Task<GetAddressByIdCommandResponse?> Handle(GetAddressByIdCommand request, CancellationToken cancellationToken)
    {
        var address = await _addressRepository.GetByIdAsync(request.Id);
        
        if (address == null)
        {
            throw new AddressNotFoundException(request.Id);
        }

        return new GetAddressByIdCommandResponse
        {
            Id = address.Id,
            ConsumerId = address.ConsumerId,
            StreetAddress = address.StreetAddress,
            City = address.City,
            State = address.State,
            PostalCode = address.PostalCode,
            Latitude = address.Latitude,
            Longitude = address.Longitude,
            IsDefault = address.IsDefault,
            CountryId = address.CountryId,
            CreatedAt = address.CreatedAt,
            UpdatedAt = address.UpdatedAt
        };
    }
} 