using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MSConsumers.Domain.Entities;
using MSConsumers.Domain.Exceptions;
using MSConsumers.Domain.Interfaces;

namespace MSConsumers.Application.Commands.Address;

/// <summary>
/// Handler para o comando de criação de endereço
/// </summary>
public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, CreateAddressCommandResponse>
{
    private readonly IAddressRepository _addressRepository;
    private readonly IConsumerRepository _consumerRepository;

    /// <summary>
    /// Inicializa uma nova instância do CreateAddressCommandHandler
    /// </summary>
    /// <param name="addressRepository">Repositório de endereços</param>
    /// <param name="consumerRepository">Repositório de consumidores</param>
    public CreateAddressCommandHandler(
        IAddressRepository addressRepository,
        IConsumerRepository consumerRepository)
    {
        _addressRepository = addressRepository;
        _consumerRepository = consumerRepository;
    }

    /// <summary>
    /// Processa o comando de criação de endereço
    /// </summary>
    /// <param name="request">O comando de criação</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>A resposta com o endereço criado</returns>
    /// <exception cref="ConsumerNotFoundException">Lançada quando o consumidor não é encontrado</exception>
    public async Task<CreateAddressCommandResponse> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        var consumer = await _consumerRepository.GetByIdAsync(request.ConsumerId);
        
        if (consumer == null)
        {
            throw new ConsumerNotFoundException(request.ConsumerId);
        }

        var address = new AddressEntity(
            request.ConsumerId,
            request.StreetAddress,
            request.City,
            request.State,
            request.PostalCode,
            request.Latitude,
            request.Longitude,
            request.IsDefault,
            request.CountryId);

        await _addressRepository.AddAsync(address);

        return new CreateAddressCommandResponse
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
            CreatedAt = address.CreatedAt,
            UpdatedAt = address.UpdatedAt,
            CountryId = address.CountryId
        };
    }
} 