using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MSConsumers.Domain.Entities;
using MSConsumers.Domain.Exceptions;
using MSConsumers.Domain.Interfaces;

namespace MSConsumers.Application.Commands.Consumer
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
            // Validate the request using DataAnnotations
            Validator.ValidateObject(request, new ValidationContext(request), validateAllProperties: true);

            var consumer = new ConsumerEntity(
                request.Name,
                request.DocumentId,
                request.PhotoUrl,
                request.PhoneNumber,
                request.Email,
                request.CurrencyId,
                request.PhoneCountryCodeId,
                request.PreferredLanguageId,
                request.TimezoneId);

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
                CreatedAt = consumer.CreatedAt,
                UpdatedAt = consumer.UpdatedAt
            };
        }
    }
} 