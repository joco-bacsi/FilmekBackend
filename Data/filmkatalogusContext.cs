using GameStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data;

public class filmkatalogusContext(DbContextOptions<filmkatalogusContext> options) : DbContext(options)
{
    public DbSet<Filmek> Filmek => Set<Filmek>();
    public DbSet<Film_Cast> Film_Casts => Set<Film_Cast>();
    public DbSet<Szineszek> szineszeks => Set<Szineszek>();

}
