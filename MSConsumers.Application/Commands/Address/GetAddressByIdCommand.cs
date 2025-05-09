using System;
using MediatR;

namespace MSConsumers.Application.Commands.Address;

/// <summary>
/// Command to get an address by ID
/// </summary>
public class GetAddressByIdCommand : IRequest<GetAddressByIdCommandResponse?>
{
    /// <summary>
    /// The address ID
    /// </summary>
    public Guid Id { get; set; }
} 