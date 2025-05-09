using System;
using System.Collections.Generic;
using MediatR;
using MSConsumers.Application.Commands.Address;

namespace MSConsumers.Application.Commands.Address;

/// <summary>
/// Command para obter endereços por ID do consumidor
/// </summary>
public class GetAddressesByConsumerIdCommand : IRequest<IEnumerable<GetAddressByIdCommandResponse>>
{
    /// <summary>
    /// ID do consumidor
    /// </summary>
    public Guid ConsumerId { get; set; }
} 