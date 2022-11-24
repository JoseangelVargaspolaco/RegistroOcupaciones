using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RegistroPagos.Models;

namespace RegistroPagos.Data;
public class Contexto : IdentityDbContext
{
    public DbSet<Ocupaciones> Ocupaciones { get; set; }
    public DbSet<Personas> Personas { get; set; }
    public DbSet<Prestamos> Prestamos { get; set; }
    public DbSet<Pagos> Pagos { get; set; }
    public DbSet<PagosDetalles> PagosDetalles {get; set;}
    public Contexto(DbContextOptions<Contexto> options) : base(options)
    {
    }

}
 
