using Microsoft.EntityFrameworkCore;
using MsConsumers.Domain.Entities;

namespace MsConsumers.Infrastructure.Data;

public interface IApplicationDbContext
{
    DbSet<ConsumerEntity> Consumers { get; set; }
    DbSet<CountryCode> CountryCodes { get; set; }
    DbSet<Currency> Currencies { get; set; }
    DbSet<Language> Languages { get; set; }
    DbSet<MsConsumers.Domain.Entities.TimeZone> TimeZones { get; set; }
    DbSet<AddressEntity> Addresses { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
} 