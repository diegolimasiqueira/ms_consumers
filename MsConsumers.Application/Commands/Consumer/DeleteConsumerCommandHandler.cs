using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MsConsumers.Domain.Exceptions;
using MsConsumers.Domain.Interfaces;

namespace MsConsumers.Application.Commands.Consumer;

/// <summary>
/// Handler for the delete consumer command
/// </summary>
public class DeleteConsumerCommandHandler : IRequestHandler<DeleteConsumerCommand, Unit>
{
    private readonly IConsumerRepository _consumerRepository;

    /// <summary>
    /// Initializes a new instance of the DeleteConsumerCommandHandler
    /// </summary>
    /// <param name="consumerRepository">Consumer repository</param>
    public DeleteConsumerCommandHandler(IConsumerRepository consumerRepository)
    {
        _consumerRepository = consumerRepository;
    }

    /// <summary>
    /// Handles the delete consumer command
    /// </summary>
    /// <param name="request">The command request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Unit value</returns>
    /// <exception cref="ConsumerNotFoundException">Thrown when the consumer is not found</exception>
    public async Task<Unit> Handle(DeleteConsumerCommand request, CancellationToken cancellationToken)
    {
        var consumer = await _consumerRepository.GetByIdAsync(request.Id);
        
        if (consumer == null)
        {
            throw new ConsumerNotFoundException(request.Id);
        }

        await _consumerRepository.DeleteAsync(request.Id);
        return Unit.Value;
    }
} 