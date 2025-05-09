using Microsoft.EntityFrameworkCore;
using MSConsumers.Domain.Entities;

namespace MSConsumers.Infrastructure.Data;

public interface IApplicationDbContext
{
    DbSet<ConsumerEntity> Consumers { get; set; }
    DbSet<CountryCode> CountryCodes { get; set; }
    DbSet<Currency> Currencies { get; set; }
    DbSet<Language> Languages { get; set; }
    DbSet<MSConsumers.Domain.Entities.TimeZone> TimeZones { get; set; }
    DbSet<AddressEntity> Addresses { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
} 