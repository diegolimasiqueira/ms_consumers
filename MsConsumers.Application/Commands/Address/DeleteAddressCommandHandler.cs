using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MsConsumers.Domain.Entities;
using MsConsumers.Domain.Exceptions;
using MsConsumers.Domain.Interfaces;

namespace MsConsumers.Application.Commands.Address;

/// <summary>
/// Handler para o comando de delete de endereço
/// </summary>
public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand, Unit>
{
    private readonly IAddressRepository _addressRepository;

    /// <summary>
    /// Inicializa uma nova instância do DeleteAddressCommandHandler
    /// </summary>
    /// <param name="addressRepository">Repositório de endereços</param>
    public DeleteAddressCommandHandler(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
    }

    /// <summary>
    /// Processa o comando de delete de endereço
    /// </summary>
    /// <param name="request">O comando de delete</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Unit</returns>
    /// <exception cref="AddressNotFoundException">Lançada quando o endereço não é encontrado</exception>
    public async Task<Unit> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
    {
        var address = await _addressRepository.GetByIdAsync(request.Id);
        
        if (address == null)
        {
            throw new AddressNotFoundException(request.Id);
        }

        await _addressRepository.DeleteAsync(address);
        return Unit.Value;
    }
} 