using Microsoft.EntityFrameworkCore;
using MSConsumers.Domain.Entities;

namespace MSConsumers.Infrastructure.Context;

/// <summary>
/// Contexto do banco de dados
/// </summary>
public class MSConsumersDbContext : DbContext
{
    /// <summary>
    /// Inicializa uma nova instância do MSConsumersDbContext
    /// </summary>
    /// <param name="options">Opções do contexto</param>
    public MSConsumersDbContext(DbContextOptions<MSConsumersDbContext> options) : base(options)
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
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ConsumerEntity>(entity =>
        {
            entity.ToTable("tb_consumers", "shc_consumer");
            entity.HasKey(e => e.Id);
            
            // Configurações de colunas com tamanho máximo e nulidade
            entity.Property(e => e.Id).HasColumnName("id").IsRequired();
            entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
            entity.Property(e => e.DocumentId).HasColumnName("document_id").HasMaxLength(50).IsRequired();
            entity.Property(e => e.PhotoUrl).HasColumnName("photo_url").HasMaxLength(500).IsRequired(false);
            entity.Property(e => e.PhoneNumber).HasColumnName("phone_number").HasMaxLength(20).IsRequired();
            entity.Property(e => e.Email).HasColumnName("email").HasMaxLength(255).IsRequired();
            entity.Property(e => e.CurrencyId).HasColumnName("currency_id").IsRequired();
            entity.Property(e => e.PhoneCountryCodeId).HasColumnName("phone_country_code_id").IsRequired();
            entity.Property(e => e.PreferredLanguageId).HasColumnName("preferred_language_id").IsRequired();
            entity.Property(e => e.TimezoneId).HasColumnName("timezone_id").IsRequired();
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired();
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").IsRequired();

            // Índices únicos
            entity.HasIndex(e => e.DocumentId).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.PhoneNumber).IsUnique();
        });

        modelBuilder.Entity<AddressEntity>(entity =>
        {
            entity.ToTable("tb_consumer_address", "shc_consumer");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id).HasColumnName("id").IsRequired();
            entity.Property(e => e.ConsumerId).HasColumnName("consumer_id").IsRequired();
            entity.Property(e => e.StreetAddress).HasColumnName("street_address").HasMaxLength(255).IsRequired();
            entity.Property(e => e.City).HasColumnName("city").HasMaxLength(30).IsRequired();
            entity.Property(e => e.State).HasColumnName("state").HasMaxLength(50).IsRequired();
            entity.Property(e => e.PostalCode).HasColumnName("postalcode").HasMaxLength(20).IsRequired();
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
            entity.Property(e => e.IsDefault).HasColumnName("is_default").IsRequired();
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired();
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").IsRequired();
            entity.Property(e => e.CountryId).HasColumnName("country_id").IsRequired();

            entity.HasOne<ConsumerEntity>(e => e.ConsumerEntity)
                .WithMany(c => c.Addresses)
                .HasForeignKey(e => e.ConsumerId)
                .HasConstraintName("FK_tb_consumer_address_consumer_id")
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne<CountryCode>(e => e.Country)
                .WithMany(c => c.Addresses)
                .HasForeignKey(e => e.CountryId)
                .HasConstraintName("FK_tb_consumer_address_country_id")
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
} 