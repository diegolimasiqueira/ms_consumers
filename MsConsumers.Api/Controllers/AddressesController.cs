using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MsConsumers.Application.Commands.Address;
using System.ComponentModel.DataAnnotations;

namespace MsConsumers.Api.Controllers;

/// <summary>
/// Controller responsável por gerenciar endereços
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AddressesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AddressesController> _logger;

    /// <summary>
    /// Inicializa uma nova instância do AddressesController
    /// </summary>
    /// <param name="mediator">Mediador para processamento de comandos</param>
    /// <param name="logger">Logger para rastreamento de eventos</param>
    public AddressesController(IMediator mediator, ILogger<AddressesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
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
        try
        {
            _logger.LogInformation("Iniciando criação de endereço para o consumidor {ConsumerId}", command.ConsumerId);
            _logger.LogDebug("Dados do endereço: {@Command}", command);

            var result = await _mediator.Send(command);

            _logger.LogInformation("Endereço criado com sucesso. ID: {AddressId}", result.Id);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar endereço para o consumidor {ConsumerId}. Detalhes: {Message}", 
                command.ConsumerId, ex.Message);
            throw;
        }
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
        try
        {
            _logger.LogInformation("Buscando endereço pelo ID: {AddressId}", id);
            
            var command = new GetAddressByIdCommand { Id = id };
            var result = await _mediator.Send(command);

            if (result == null)
            {
                _logger.LogWarning("Endereço não encontrado para o ID: {AddressId}", id);
                return NotFound();
            }

            _logger.LogInformation("Endereço encontrado: {@Address}", result);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar endereço pelo ID {AddressId}. Detalhes: {Message}", 
                id, ex.Message);
            throw;
        }
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
        try
        {
            _logger.LogInformation("Buscando endereços do consumidor {ConsumerId}", consumerId);
            
            var command = new GetAddressesByConsumerIdCommand { ConsumerId = consumerId };
            var result = await _mediator.Send(command);

            _logger.LogInformation("Encontrados {Count} endereços para o consumidor {ConsumerId}", 
                result.Count(), consumerId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar endereços do consumidor {ConsumerId}. Detalhes: {Message}", 
                consumerId, ex.Message);
            throw;
        }
    }
} 