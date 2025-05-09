using System;
using MediatR;

namespace MSConsumers.Application.Commands.Consumer;

/// <summary>
/// Command to get a consumer by ID
/// </summary>
public class GetConsumerByIdCommand : IRequest<GetConsumerByIdCommandResponse>
{
    /// <summary>
    /// Consumer ID
    /// </summary>
    public Guid Id { get; set; }
} 