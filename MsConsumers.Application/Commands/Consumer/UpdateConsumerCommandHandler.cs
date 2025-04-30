using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MsConsumers.Domain.Entities;
using MsConsumers.Domain.Exceptions;
using MsConsumers.Domain.Interfaces;

namespace MsConsumers.Application.Commands.Consumer;

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
    /// <exception cref="ConsumerNotFoundException">Thrown when the consumer is not found</exception>
    public async Task<UpdateConsumerCommandResponse> Handle(UpdateConsumerCommand request, CancellationToken cancellationToken)
    {
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