using System;
using MediatR;

namespace MsConsumers.Application.Commands.Consumer;

/// <summary>
/// Command to delete a consumer
/// </summary>
public class DeleteConsumerCommand : IRequest<Unit>
{
    /// <summary>
    /// Consumer ID
    /// </summary>
    public Guid Id { get; set; }
} 