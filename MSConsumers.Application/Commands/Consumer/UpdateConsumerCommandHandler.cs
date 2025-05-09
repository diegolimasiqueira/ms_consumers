using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MSConsumers.Domain.Entities;
using MSConsumers.Domain.Exceptions;
using MSConsumers.Domain.Interfaces;

namespace MSConsumers.Application.Commands.Consumer;

/// <summary>
/// Handler for the update consumer command
/// </summary>
public class UpdateConsumerCommandHandler : IRequestHandler<UpdateConsumerCommand, UpdateConsumerCommandResponse>
{
    private readonly IConsumerRepository _consumerRepository;

    /// <summary>
    /// Initializes a new instance of the UpdateConsumerCommandHandler
    /// </summary>
    /// <param name="consumerRepository">Consumer repository</param>
    public UpdateConsumerCommandHandler(IConsumerRepository consumerRepository)
    {
        _consumerRepository = consumerRepository;
    }

    /// <summary>
    /// Handles the update consumer command
    /// </summary>
    /// <param name="request">The command request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated consumer response</returns>
    /// <exception cref="ArgumentException">Thrown when the ID is empty</exception>
    /// <exception cref="ConsumerNotFoundException">Thrown when the consumer is not found</exception>
    public async Task<UpdateConsumerCommandResponse> Handle(UpdateConsumerCommand request, CancellationToken cancellationToken)
    {
        if (request.Id == Guid.Empty)
        {
            throw new ArgumentException("Consumer ID cannot be empty", nameof(request.Id));
        }

        var consumer = await _consumerRepository.GetByIdAsync(request.Id);
        
        if (consumer == null)
        {
            throw new ConsumerNotFoundException(request.Id);
        }

        consumer.Update(
            request.Name,
            request.DocumentId,
            request.PhotoUrl,
            request.PhoneNumber,
            request.Email,
            request.CurrencyId,
            request.PhoneCountryCodeId,
            request.PreferredLanguageId,
            request.TimezoneId
        );

        await _consumerRepository.UpdateAsync(consumer);

        return new UpdateConsumerCommandResponse
        {
            Id = consumer.Id,
            Name = consumer.Name,
            DocumentId = consumer.DocumentId,
            PhotoUrl = consumer.PhotoUrl,
            PhoneNumber = consumer.PhoneNumber,
            Email = consumer.Email,
            CurrencyId = consumer.CurrencyId,
            PhoneCountryCodeId = consumer.PhoneCountryCodeId,
            PreferredLanguageId = consumer.PreferredLanguageId,
            TimezoneId = consumer.TimezoneId,
            CreatedAt = consumer.CreatedAt,
            UpdatedAt = consumer.UpdatedAt
        };
    }
} 