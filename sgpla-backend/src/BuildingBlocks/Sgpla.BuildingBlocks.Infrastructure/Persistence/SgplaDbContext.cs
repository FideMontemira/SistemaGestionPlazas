using Microsoft.EntityFrameworkCore;

namespace Sgpla.BuildingBlocks.Infrastructure.Persistence;

/// <summary>
/// Contexto único de EF Core para toda la aplicación. No define entidades propias:
/// cada módulo aporta sus <see cref="IEntityTypeConfiguration{TEntity}"/> y el contexto
/// las aplica desde los ensamblados registrados con <see cref="PersistenciaExtensions.AddPersistenciaModulo"/>.
/// El esquema físico lo administra DbUp (proyecto Sgpla.Database); aquí no hay EF Migrations.
/// </summary>
public sealed class SgplaDbContext(
    DbContextOptions<SgplaDbContext> options,
    IEnumerable<EnsambladoModulo> ensamblados) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var ensamblado in ensamblados)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(ensamblado.Ensamblado);
        }
    }
}
