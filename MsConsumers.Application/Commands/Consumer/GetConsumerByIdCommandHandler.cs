using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MsConsumers.Domain.Entities;
using MsConsumers.Domain.Exceptions;
using MsConsumers.Domain.Interfaces;

namespace MsConsumers.Application.Commands.Consumer;

/// <summary>
/// Handler for the get consumer by ID command
/// </summary>
public class GetConsumerByIdCommandHandler : IRequestHandler<GetConsumerByIdCommand, GetConsumerByIdCommandResponse?>
{
    private readonly IConsumerRepository _consumerRepository;

    /// <summary>
    /// Initializes a new instance of the GetConsumerByIdCommandHandler
    /// </summary>
    /// <param name="consumerRepository">Consumer repository</param>
    public GetConsumerByIdCommandHandler(IConsumerRepository consumerRepository)
    {
        _consumerRepository = consumerRepository;
    }

    /// <summary>
    /// Handles the get consumer by ID command
    /// </summary>
    /// <param name="request">The command request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The consumer response</returns>
    /// <exception cref="ArgumentException">Thrown when the ID is empty</exception>
    /// <exception cref="ConsumerNotFoundException">Thrown when the consumer is not found</exception>
    public async Task<GetConsumerByIdCommandResponse?> Handle(GetConsumerByIdCommand request, CancellationToken cancellationToken)
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

        return new GetConsumerByIdCommandResponse
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