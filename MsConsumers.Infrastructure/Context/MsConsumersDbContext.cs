using Microsoft.EntityFrameworkCore;
using MsConsumers.Domain.Entities;

namespace MsConsumers.Infrastructure.Context;

/// <summary>
/// Contexto do banco de dados
/// </summary>
public class MsConsumersDbContext : DbContext
{
    /// <summary>
    /// Inicializa uma nova instância do MsConsumersDbContext
    /// </summary>
    /// <param name="options">Opções do contexto</param>
    public MsConsumersDbContext(DbContextOptions<MsConsumersDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Tabela de consumidores
    /// </summary>
    public DbSet<ConsumerEntity> Consumers { get; set; }

    /// <summary>
    /// Tabela de endereços
    /// </summary>
    public DbSet<AddressEntity> Addresses { get; set; }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MsConsumersDbContext).Assembly);
    }
} 