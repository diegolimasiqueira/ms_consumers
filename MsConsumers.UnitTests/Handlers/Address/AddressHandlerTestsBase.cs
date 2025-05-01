using System;
using Moq;
using Moq.Language.Flow;
using MsConsumers.Domain.Entities;
using MsConsumers.Domain.Interfaces;

namespace MsConsumers.UnitTests.Handlers.Address;

public abstract class AddressHandlerTestsBase
{
    protected readonly Mock<IAddressRepository> _addressRepositoryMock;
    protected readonly Mock<IConsumerRepository> _consumerRepositoryMock;

    protected AddressHandlerTestsBase()
    {
        _addressRepositoryMock = new Mock<IAddressRepository>();
        _consumerRepositoryMock = new Mock<IConsumerRepository>();
    }

    protected AddressEntity CreateValidAddress(Guid? consumerId = null)
    {
        return new AddressEntity(
            consumerId ?? Guid.NewGuid(), // consumerId
            "Rua Teste, 123", // streetAddress
            "São Paulo", // city
            "SP", // state
            "12345-678", // postalCode
            23.5505, // latitude
            46.6333, // longitude
            true, // isDefault
            Guid.NewGuid() // countryId
        );
    }

    protected ConsumerEntity CreateValidConsumer()
    {
        var currencyId = Guid.NewGuid();
        var phoneCountryCodeId = Guid.NewGuid();
        var preferredLanguageId = Guid.NewGuid();
        var timezoneId = Guid.NewGuid();

        return new ConsumerEntity(
            "John Doe", // name
            "123456789", // documentId
            null, // photoUrl
            "11999999999", // phoneNumber
            "john.doe@example.com", // email
            currencyId, // currencyId
            phoneCountryCodeId, // phoneCountryCodeId
            preferredLanguageId, // preferredLanguageId
            timezoneId // timezoneId
        );
    }
} 