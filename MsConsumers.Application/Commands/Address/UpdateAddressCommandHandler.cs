using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MsConsumers.Domain.Entities;
using MsConsumers.Domain.Exceptions;
using MsConsumers.Domain.Interfaces;

namespace MsConsumers.Application.Commands.Address;

/// <summary>
/// Handler para o comando de atualização de endereço
/// </summary>
public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, UpdateAddressCommandResponse>
{
    private readonly IAddressRepository _addressRepository;

    /// <summary>
    /// Inicializa uma nova instância do UpdateAddressCommandHandler
    /// </summary>
    /// <param name="addressRepository">Repositório de endereços</param>
    public UpdateAddressCommandHandler(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
    }

    /// <summary>
    /// Processa o comando de atualização de endereço
    /// </summary>
    /// <param name="request">O comando de atualização</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>A resposta com o endereço atualizado</returns>
    /// <exception cref="AddressNotFoundException">Lançada quando o endereço não é encontrado</exception>
    public async Task<UpdateAddressCommandResponse> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        var address = await _addressRepository.GetByIdAsync(request.Id);
        
        if (address == null)
        {
            throw new AddressNotFoundException(request.Id);
        }

        address.Update(
            request.StreetAddress,
            request.City,
            request.State,
            request.PostalCode,
            request.Latitude,
            request.Longitude,
            request.IsDefault,
            request.CountryId);

        await _addressRepository.UpdateAsync(address);

        return new UpdateAddressCommandResponse
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