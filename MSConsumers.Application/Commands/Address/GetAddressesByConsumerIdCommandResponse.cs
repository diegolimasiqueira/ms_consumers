using System;

namespace MSConsumers.Application.Commands.Address;

/// <summary>
/// Response para o comando de obter endereços por ID do consumidor
/// </summary>
public class GetAddressesByConsumerIdCommandResponse
{
    /// <summary>
    /// ID do endereço
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// ID do consumidor
    /// </summary>
    public Guid ConsumerId { get; set; }

    /// <summary>
    /// CEP do endereço
    /// </summary>
    public string ZipCode { get; set; } = string.Empty;

    /// <summary>
    /// Logradouro
    /// </summary>
    public string Street { get; set; } = string.Empty;

    /// <summary>
    /// Número
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Complemento
    /// </summary>
    public string? Complement { get; set; }

    /// <summary>
    /// Bairro
    /// </summary>
    public string Neighborhood { get; set; } = string.Empty;

    /// <summary>
    /// Cidade
    /// </summary>
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// Estado
    /// </summary>
    public string State { get; set; } = string.Empty;

    /// <summary>
    /// País
    /// </summary>
    public string Country { get; set; } = string.Empty;

    /// <summary>
    /// Indica se é o endereço principal
    /// </summary>
    public bool IsMain { get; set; }

    /// <summary>
    /// Data de criação
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Data de atualização
    /// </summary>
    public DateTime UpdatedAt { get; set; }
} 