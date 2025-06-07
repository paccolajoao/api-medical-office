using Microsoft.EntityFrameworkCore;
using ApiMedicalOffice.Domain.Entities;

namespace ApiMedicalOffice.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Paciente> Pacientes => Set<Paciente>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.Property(e => e.Id)
                  .HasColumnType("bigint unsigned");

            entity.Property(e => e.Nome)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.NomePai)
                  .HasMaxLength(100)
                  .HasDefaultValue(null)
                  .IsRequired(false);

            entity.Property(e => e.NomeMae)
                  .HasMaxLength(100)
                  .HasDefaultValue(null)
                  .IsRequired(false);

            entity.Property(e => e.Celular)
                  .IsRequired()
                  .HasMaxLength(20);

            entity.Property(e => e.Email)
                  .HasMaxLength(120)
                  .HasDefaultValue(null)
                  .IsRequired(false);

            entity.Property(e => e.DataNascimento)
                  .IsRequired();
        });
    }
}
