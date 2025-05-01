using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MsConsumers.Domain.Entities;
using MsConsumers.Domain.Interfaces;
using MsConsumers.Infrastructure.Data;

namespace MsConsumers.Infrastructure.Repositories
{
    public class ConsumerRepository : IConsumerRepository
    {
        private readonly IApplicationDbContext _context;

        public ConsumerRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ConsumerEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Consumers.FindAsync(id);
        }

        public async Task<IEnumerable<ConsumerEntity>> GetAllAsync()
        {
            return await _context.Consumers.ToListAsync();
        }

        public async Task AddAsync(ConsumerEntity consumer)
        {
            await _context.Consumers.AddAsync(consumer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ConsumerEntity consumer)
        {
            _context.Consumers.Update(consumer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var consumer = await GetByIdAsync(id);
            if (consumer != null)
            {
                _context.Consumers.Remove(consumer);
                await _context.SaveChangesAsync();
            }
        }
    }
} 