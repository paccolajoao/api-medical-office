using Microsoft.EntityFrameworkCore;
using ApiMedicalOffice.Models;

namespace ApiMedicalOffice.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Paciente> Pacientes => Set<Paciente>();
}
