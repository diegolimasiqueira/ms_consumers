using System;
using MediatR;

namespace MSConsumers.Application.Commands.Consumer;

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