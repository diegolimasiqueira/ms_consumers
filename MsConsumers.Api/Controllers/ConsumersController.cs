using MediatR;
using Microsoft.AspNetCore.Mvc;
using MsConsumers.Application.Commands.Consumer;
using System.ComponentModel.DataAnnotations;

namespace MsConsumers.Api.Controllers;

/// <summary>
/// Controller responsible for managing consumer operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ConsumersController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of the ConsumersController
    /// </summary>
    /// <param name="mediator">Mediator for command processing</param>
    public ConsumersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new consumer
    /// </summary>
    /// <param name="command">Consumer data to be created</param>
    /// <returns>The created consumer with its ID</returns>
    /// <response code="201">Returns the newly created consumer</response>
    /// <response code="400">If the consumer data is invalid</response>
    /// <response code="500">If an internal server error occurs</response>
    [HttpPost]
    [ProducesResponseType(typeof(CreateConsumerCommandResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateConsumerCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Gets a consumer by its ID
    /// </summary>
    /// <param name="id">Consumer ID</param>
    /// <returns>The found consumer</returns>
    /// <response code="200">Returns the requested consumer</response>
    /// <response code="404">If the consumer is not found</response>
    /// <response code="500">If an internal server error occurs</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetConsumerByIdCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById([Required] Guid id)
    {
        var command = new GetConsumerByIdCommand { Id = id };
        var result = await _mediator.Send(command);
        
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes a consumer by its ID
    /// </summary>
    /// <param name="id">Consumer ID</param>
    /// <returns>No content</returns>
    /// <response code="204">If the consumer was successfully deleted</response>
    /// <response code="404">If the consumer is not found</response>
    /// <response code="500">If an internal server error occurs</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([Required] Guid id)
    {
        var command = new DeleteConsumerCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Atualiza um consumidor existente
    /// </summary>
    /// <param name="id">ID do consumidor</param>
    /// <param name="command">Dados do consumidor para atualização</param>
    /// <returns>O consumidor atualizado</returns>
    /// <response code="200">Retorna o consumidor atualizado</response>
    /// <response code="400">Se o ID não for fornecido ou os dados forem inválidos</response>
    /// <response code="404">Se o consumidor não for encontrado</response>
    /// <response code="500">Se ocorrer um erro interno</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(UpdateConsumerCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateConsumerCommand command)
    {
        if (id == Guid.Empty)
        {
            return BadRequest("ID do consumidor é obrigatório");
        }

        command.Id = id;
        var result = await _mediator.Send(command);
        return Ok(result);
    }
} 