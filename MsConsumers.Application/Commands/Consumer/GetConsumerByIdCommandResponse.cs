using System;

namespace MsConsumers.Application.Commands.Consumer;

/// <summary>
/// Response for the get consumer by ID command
/// </summary>
public class GetConsumerByIdCommandResponse
{
    /// <summary>
    /// Consumer ID
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Consumer's name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Consumer's document ID
    /// </summary>
    public string DocumentId { get; set; } = string.Empty;

    /// <summary>
    /// Consumer's photo URL
    /// </summary>
    public string? PhotoUrl { get; set; }

    /// <summary>
    /// Consumer's phone number
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Consumer's email
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Consumer's currency ID
    /// </summary>
    public Guid CurrencyId { get; set; }

    /// <summary>
    /// Consumer's phone country code ID
    /// </summary>
    public Guid PhoneCountryCodeId { get; set; }

    /// <summary>
    /// Consumer's preferred language ID
    /// </summary>
    public Guid PreferredLanguageId { get; set; }

    /// <summary>
    /// Consumer's timezone ID
    /// </summary>
    public Guid TimezoneId { get; set; }

    /// <summary>
    /// Consumer's creation date
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Consumer's update date
    /// </summary>
    public DateTime UpdatedAt { get; set; }
} 