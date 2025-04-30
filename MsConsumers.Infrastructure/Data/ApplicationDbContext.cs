using Microsoft.EntityFrameworkCore;
using MsConsumers.Domain.Entities;

namespace MsConsumers.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Consumer> Consumers { get; set; }
    public DbSet<ConsumerAddress> ConsumerAddresses { get; set; }
    public DbSet<CountryCode> CountryCodes { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<MsConsumers.Domain.Entities.TimeZone> TimeZones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Consumer>(entity =>
        {
            entity.ToTable("tb_consumers", "shc_consumer");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(100);
            entity.Property(e => e.DocumentId).HasColumnName("document_id").HasMaxLength(50);
            entity.Property(e => e.PhotoUrl).HasColumnName("photo_url").HasMaxLength(500);
            entity.Property(e => e.PhoneNumber).HasColumnName("phone_number").HasMaxLength(20);
            entity.Property(e => e.Email).HasColumnName("email").HasMaxLength(255);
            entity.Property(e => e.CurrencyId).HasColumnName("currency_id");
            entity.Property(e => e.PhoneCountryCodeId).HasColumnName("phone_country_code_id");
            entity.Property(e => e.PreferredLanguageId).HasColumnName("preferred_language_id");
            entity.Property(e => e.TimezoneId).HasColumnName("timezone_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

            entity.HasIndex(e => e.DocumentId).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.PhoneNumber).IsUnique();
        });

        modelBuilder.Entity<ConsumerAddress>(entity =>
        {
            entity.ToTable("tb_consumer_address", "shc_consumer");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ConsumerId).HasColumnName("consumer_id");
            entity.Property(e => e.StreetAddress).HasColumnName("street_address").HasMaxLength(255);
            entity.Property(e => e.City).HasColumnName("city").HasMaxLength(30);
            entity.Property(e => e.State).HasColumnName("state").HasMaxLength(50);
            entity.Property(e => e.PostalCode).HasColumnName("postalcode").HasMaxLength(20);
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
            entity.Property(e => e.IsDefault).HasColumnName("is_default");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.CountryId).HasColumnName("country_id");

            entity.HasOne(e => e.Consumer)
                .WithMany(e => e.Addresses)
                .HasForeignKey(e => e.ConsumerId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Country)
                .WithMany(e => e.Addresses)
                .HasForeignKey(e => e.CountryId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<CountryCode>(entity =>
        {
            entity.ToTable("tb_country_codes", "shc_consumer");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code).HasColumnName("code").HasMaxLength(5);
            entity.Property(e => e.CountryName).HasColumnName("country_name").HasMaxLength(100);

            entity.HasIndex(e => e.Code).IsUnique();
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.ToTable("tb_currencies", "shc_consumer");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code).HasColumnName("code").HasMaxLength(3);
            entity.Property(e => e.Description).HasColumnName("description").HasMaxLength(100);

            entity.HasIndex(e => e.Code).IsUnique();
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.ToTable("tb_languages", "shc_consumer");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code).HasColumnName("code").HasMaxLength(10);
            entity.Property(e => e.Description).HasColumnName("description").HasMaxLength(100);

            entity.HasIndex(e => e.Code).IsUnique();
        });

        modelBuilder.Entity<MsConsumers.Domain.Entities.TimeZone>(entity =>
        {
            entity.ToTable("tb_time_zones", "shc_consumer");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(50);
            entity.Property(e => e.Description).HasColumnName("description").HasMaxLength(100);

            entity.HasIndex(e => e.Name).IsUnique();
        });
    }
} 