using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace MsConsumers.Application.Commands.Consumer;

/// <summary>
/// Command to update a consumer
/// </summary>
public class UpdateConsumerCommand : IRequest<UpdateConsumerCommandResponse>
{
    /// <summary>
    /// Consumer ID
    /// </summary>
    [Required(ErrorMessage = "ID is required")]
    public Guid Id { get; set; }

    /// <summary>
    /// Consumer's name
    /// </summary>
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name must have a maximum of 100 characters")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Consumer's document ID
    /// </summary>
    [Required(ErrorMessage = "Document is required")]
    [StringLength(20, ErrorMessage = "Document must have a maximum of 20 characters")]
    public string DocumentId { get; set; } = string.Empty;

    /// <summary>
    /// Consumer's photo URL
    /// </summary>
    [StringLength(500, ErrorMessage = "Photo URL must have a maximum of 500 characters")]
    public string PhotoUrl { get; set; } = string.Empty;

    /// <summary>
    /// Consumer's phone number
    /// </summary>
    [Required(ErrorMessage = "Phone number is required")]
    [StringLength(20, ErrorMessage = "Phone number must have a maximum of 20 characters")]
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// Consumer's email
    /// </summary>
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    [StringLength(100, ErrorMessage = "Email must have a maximum of 100 characters")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Consumer's currency ID
    /// </summary>
    [Required(ErrorMessage = "Currency is required")]
    public Guid CurrencyId { get; set; }

    /// <summary>
    /// Consumer's phone country code ID
    /// </summary>
    [Required(ErrorMessage = "Country code is required")]
    public Guid PhoneCountryCodeId { get; set; }

    /// <summary>
    /// Consumer's preferred language ID
    /// </summary>
    [Required(ErrorMessage = "Preferred language is required")]
    public Guid PreferredLanguageId { get; set; }

    /// <summary>
    /// Consumer's timezone ID
    /// </summary>
    [Required(ErrorMessage = "Timezone is required")]
    public Guid TimezoneId { get; set; }
} 