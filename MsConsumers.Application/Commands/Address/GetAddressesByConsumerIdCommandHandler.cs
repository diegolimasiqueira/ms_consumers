using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MsConsumers.Domain.Entities;
using MsConsumers.Domain.Exceptions;
using MsConsumers.Domain.Interfaces;

namespace MsConsumers.Application.Commands.Address;

/// <summary>
/// Handler para o comando de obter endereços por ID do consumidor
/// </summary>
public class GetAddressesByConsumerIdCommandHandler : IRequestHandler<GetAddressesByConsumerIdCommand, IEnumerable<GetAddressByIdCommandResponse>>
{
    private readonly IAddressRepository _addressRepository;

    /// <summary>
    /// Inicializa uma nova instância do GetAddressesByConsumerIdCommandHandler
    /// </summary>
    /// <param name="addressRepository">Repositório de endereços</param>
    public GetAddressesByConsumerIdCommandHandler(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    /// <summary>
    /// Processa o comando de obter endereços por ID do consumidor
    /// </summary>
    /// <param name="request">O comando de consulta</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>A resposta com a lista de endereços encontrados</returns>
    public async Task<IEnumerable<GetAddressByIdCommandResponse>> Handle(GetAddressesByConsumerIdCommand request, CancellationToken cancellationToken)
    {
        var addresses = await _addressRepository.GetByConsumerIdAsync(request.ConsumerId);
        
        if (!addresses.Any())
        {
            throw new AddressNotFoundException(Guid.Empty);
        }

        return addresses.Select(address => new GetAddressByIdCommandResponse
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
        });
    }
} 