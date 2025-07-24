using Microsoft.EntityFrameworkCore;
using ConsultaRUCAPI.Models;

namespace ConsultaRUCAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }

    public DbSet<Persona> Personas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuario>().ToTable("Usuarios");

        // Como Persona no tiene una tabla física ni clave, se configura así:
        modelBuilder.Entity<Persona>().HasNoKey().ToView(null);
    }
}

