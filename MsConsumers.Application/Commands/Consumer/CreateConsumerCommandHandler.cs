using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MsConsumers.Domain.Entities;
using MsConsumers.Domain.Interfaces;

namespace MsConsumers.Application.Commands.Consumer
{
    public class CreateConsumerCommandHandler : IRequestHandler<CreateConsumerCommand, CreateConsumerCommandResponse>
    {
        private readonly IConsumerRepository _consumerRepository;

        public CreateConsumerCommandHandler(IConsumerRepository consumerRepository)
        {
            _consumerRepository = consumerRepository;
        }

        public async Task<CreateConsumerCommandResponse> Handle(CreateConsumerCommand request, CancellationToken cancellationToken)
        {
            var consumer = new ConsumerEntity(
                id: Guid.NewGuid(),
                name: request.Name,
                documentId: request.DocumentId,
                photoUrl: request.PhotoUrl,
                phoneNumber: request.PhoneNumber,
                email: request.Email,
                currencyId: request.CurrencyId,
                phoneCountryCodeId: request.PhoneCountryCodeId,
                preferredLanguageId: request.PreferredLanguageId,
                timezoneId: request.TimezoneId
            );

            await _consumerRepository.AddAsync(consumer);

            return new CreateConsumerCommandResponse
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
                CreatedAt = consumer.CreatedAt
            };
        }
    }
} 