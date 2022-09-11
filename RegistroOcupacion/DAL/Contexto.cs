using Microsoft.EntityFrameworkCore;
using RegistroOcupacion.Entidades;
using RegistroOcupacion.DAL;

namespace RegistroOcupacion.DAL{
public class Contexto : DbContext
{
    public DbSet<Ocupaciones> Ocupaciones { get; set; }

    public Contexto(DbContextOptions<Contexto> options) : base(options)
    {
    }

}
}
 