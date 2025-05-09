using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MSConsumers.Domain.Entities;

namespace MSConsumers.Domain.Interfaces;

/// <summary>
/// Interface para o repositório de endereços
/// </summary>
public interface IAddressRepository
{
    /// <summary>
    /// Adiciona um novo endereço
    /// </summary>
    /// <param name="address">O endereço a ser adicionado</param>
    /// <returns>Task</returns>
    Task AddAsync(AddressEntity address);

    /// <summary>
    /// Atualiza um endereço existente
    /// </summary>
    /// <param name="address">O endereço a ser atualizado</param>
    /// <returns>Task</returns>
    Task UpdateAsync(AddressEntity address);

    /// <summary>
    /// Remove um endereço
    /// </summary>
    /// <param name="address">O endereço a ser removido</param>
    /// <returns>Task</returns>
    Task DeleteAsync(AddressEntity address);

    /// <summary>
    /// Obtém um endereço pelo ID
    /// </summary>
    /// <param name="id">ID do endereço</param>
    /// <returns>O endereço encontrado ou null</returns>
    Task<AddressEntity?> GetByIdAsync(Guid id);

    /// <summary>
    /// Obtém todos os endereços de um consumidor
    /// </summary>
    /// <param name="consumerId">ID do consumidor</param>
    /// <returns>Lista de endereços do consumidor</returns>
    Task<IEnumerable<AddressEntity>> GetByConsumerIdAsync(Guid consumerId);

    /// <summary>
    /// Obtém o endereço principal de um consumidor
    /// </summary>
    /// <param name="consumerId">ID do consumidor</param>
    /// <returns>O endereço principal ou null</returns>
    Task<AddressEntity?> GetMainAddressByConsumerIdAsync(Guid consumerId);
} 