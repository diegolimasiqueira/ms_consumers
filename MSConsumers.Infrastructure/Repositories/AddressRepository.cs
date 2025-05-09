using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MSConsumers.Domain.Entities;
using MSConsumers.Domain.Interfaces;
using MSConsumers.Infrastructure.Data;

namespace MSConsumers.Infrastructure.Repositories;

/// <summary>
/// Implementação do repositório de endereços
/// </summary>
public class AddressRepository : IAddressRepository
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Inicializa uma nova instância do AddressRepository
    /// </summary>
    /// <param name="context">Contexto do banco de dados</param>
    public AddressRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task AddAsync(AddressEntity address)
    {
        await _context.Addresses.AddAsync(address);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task UpdateAsync(AddressEntity address)
    {
        _context.Entry(address).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task DeleteAsync(AddressEntity address)
    {
        _context.Addresses.Remove(address);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task<AddressEntity?> GetByIdAsync(Guid id)
    {
        return await _context.Addresses
            .Include(a => a.Country)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<AddressEntity>> GetByConsumerIdAsync(Guid consumerId)
    {
        return await _context.Addresses
            .Include(a => a.Country)
            .Where(a => a.ConsumerId == consumerId)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<AddressEntity?> GetMainAddressByConsumerIdAsync(Guid consumerId)
    {
        return await _context.Addresses
            .Include(a => a.Country)
            .FirstOrDefaultAsync(a => a.ConsumerId == consumerId && a.IsDefault);
    }
} 