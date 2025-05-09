using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace MSConsumers.Application.Commands.Address;

/// <summary>
/// Command para deletar um endereço
/// </summary>
public class DeleteAddressCommand : IRequest<Unit>
{
    /// <summary>
    /// ID do endereço
    /// </summary>
    [Required]
    public Guid Id { get; set; }
} 