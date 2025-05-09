using MediatR;
using Microsoft.AspNetCore.Mvc;
using MSConsumers.Application.Commands.Address;
using System.ComponentModel.DataAnnotations;

namespace MSConsumers.Api.Controllers;

/// <summary>
/// Controller responsável por gerenciar endereços
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AddressesController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Inicializa uma nova instância do AddressesController
    /// </summary>
    /// <param name="mediator">Mediador para processamento de comandos</param>
    public AddressesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Cria um novo endereço
    /// </summary>
    /// <param name="command">Dados do endereço a ser criado</param>
    /// <returns>O endereço criado</returns>
    /// <response code="201">Retorna o endereço recém-criado</response>
    /// <response code="400">Se os dados do endereço forem inválidos</response>
    /// <response code="404">Se o consumidor não for encontrado</response>
    /// <response code="500">Se ocorrer um erro interno</response>
    [HttpPost]
    [ProducesResponseType(typeof(CreateAddressCommandResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateAddressCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Gets an address by ID
    /// </summary>
    /// <param name="id">The address ID</param>
    /// <returns>The address details</returns>
    /// <response code="200">Returns the address details</response>
    /// <response code="404">If the address is not found</response>
    /// <response code="500">Se ocorrer um erro interno</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetAddressByIdCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var command = new GetAddressByIdCommand { Id = id };
        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Obtém todos os endereços de um consumidor
    /// </summary>
    /// <param name="consumerId">ID do consumidor</param>
    /// <returns>Lista de endereços do consumidor</returns>
    /// <response code="200">Retorna a lista de endereços</response>
    /// <response code="500">Se ocorrer um erro interno</response>
    [HttpGet("consumer/{consumerId}")]
    [ProducesResponseType(typeof(IEnumerable<GetAddressByIdCommandResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByConsumerId([Required] Guid consumerId)
    {
        var command = new GetAddressesByConsumerIdCommand { ConsumerId = consumerId };
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Atualiza um endereço existente
    /// </summary>
    /// <param name="command">Comando de atualização de endereço</param>
    /// <returns>O endereço atualizado</returns>
    [HttpPut]
    [ProducesResponseType(typeof(UpdateAddressCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UpdateAddressCommandResponse>> Update([FromBody] UpdateAddressCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    /// <summary>
    /// Deleta um endereço existente
    /// </summary>
    /// <param name="id">ID do endereço a ser deletado</param>
    /// <returns>No content</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteAddressCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
} 