using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MsConsumers.Domain.Entities;

namespace MsConsumers.Domain.Interfaces
{
    public interface IConsumerRepository
    {
        Task<ConsumerEntity> GetByIdAsync(Guid id);
        Task<IEnumerable<ConsumerEntity>> GetAllAsync();
        Task AddAsync(ConsumerEntity consumer);
        Task UpdateAsync(ConsumerEntity consumer);
        Task DeleteAsync(Guid id);
    }
} 