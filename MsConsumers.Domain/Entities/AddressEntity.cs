using System;

namespace MsConsumers.Domain.Entities;

/// <summary>
/// Entidade que representa um endereço
/// </summary>
public class AddressEntity
{
    /// <summary>
    /// ID do endereço
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// ID do consumidor
    /// </summary>
    public Guid ConsumerId { get; private set; }

    /// <summary>
    /// Endereço completo
    /// </summary>
    public string StreetAddress { get; private set; }

    /// <summary>
    /// Cidade
    /// </summary>
    public string City { get; private set; }

    /// <summary>
    /// Estado
    /// </summary>
    public string State { get; private set; }

    /// <summary>
    /// CEP
    /// </summary>
    public string PostalCode { get; private set; }

    /// <summary>
    /// Latitude
    /// </summary>
    public double? Latitude { get; private set; }

    /// <summary>
    /// Longitude
    /// </summary>
    public double? Longitude { get; private set; }

    /// <summary>
    /// Indica se é o endereço padrão
    /// </summary>
    public bool IsDefault { get; private set; }

    /// <summary>
    /// ID do país
    /// </summary>
    public Guid CountryId { get; private set; }

    /// <summary>
    /// Data de criação
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Data de atualização
    /// </summary>
    public DateTime UpdatedAt { get; private set; }

    // Propriedades de navegação
    public ConsumerEntity? ConsumerEntity { get; private set; }
    public CountryCode? Country { get; private set; }

    /// <summary>
    /// Inicializa uma nova instância da entidade AddressEntity
    /// </summary>
    public AddressEntity(
        Guid consumerId,
        string streetAddress,
        string city,
        string state,
        string postalCode,
        double? latitude,
        double? longitude,
        bool isDefault,
        Guid countryId)
    {
        Id = Guid.NewGuid();
        ConsumerId = consumerId;
        StreetAddress = streetAddress;
        City = city;
        State = state;
        PostalCode = postalCode;
        Latitude = latitude;
        Longitude = longitude;
        IsDefault = isDefault;
        CountryId = countryId;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Atualiza os dados do endereço
    /// </summary>
    public void Update(
        string streetAddress,
        string city,
        string state,
        string postalCode,
        double? latitude,
        double? longitude,
        bool isDefault,
        Guid countryId)
    {
        StreetAddress = streetAddress;
        City = city;
        State = state;
        PostalCode = postalCode;
        Latitude = latitude;
        Longitude = longitude;
        IsDefault = isDefault;
        CountryId = countryId;
        UpdatedAt = DateTime.UtcNow;
    }
} 