using dsw1_t1_Alburqueque_Jose.Models;
using Microsoft.EntityFrameworkCore;

namespace dsw1_t1_Alburqueque_Jose.Data
{
  public class AplicationsContext : DbContext
  {
    public AplicationsContext(DbContextOptions<AplicationsContext> options) : base(options)
    {

    }

    public DbSet<Curso> Cursos { get; set; }
    public DbSet<NivelAcademico> NivelAcademicos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<NivelAcademico>()
                .HasMany(n => n.Cursos)
                .WithOne(c => c.NivelAcademico)
                .HasForeignKey(c => c.NivelAcademicoId);
    }
  }
}