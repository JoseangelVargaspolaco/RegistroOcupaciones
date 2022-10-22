using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RegistroPagos.Data;
using RegistroPagos.Models;

namespace RegistroPagos.BLL
{
     public class PrestamosBLL
     {
          private Contexto contextoP;
          public PrestamosBLL(Contexto contexto)
          {
               contextoP = contexto;
          }

          public async Task<bool> Existe(int prestamosId)
          {
               return await contextoP.Prestamos.AnyAsync(o => o.PrestamoId == prestamosId);
          }

          public async Task<bool> Guardar(Prestamos prestamos)
          {
               if (!await Existe(prestamos.PrestamoId))
                    return await this.Insertar(prestamos);
               else
                    return await this.Modificar(prestamos);
          }

          public async Task<bool> Eliminar(Prestamos prestamos)
          {
            var personas = await contextoP.Personas.FindAsync(prestamos.PersonaId);
            personas.Balance -= prestamos.Monto;

            contextoP.Entry(prestamos).State = EntityState.Deleted;
            return await contextoP.SaveChangesAsync() > 0;
          }
          public async Task<bool> Insertar(Prestamos prestamos)
          {
            await contextoP.Prestamos.AddAsync(prestamos);
            
            var persona = await contextoP.Personas.FindAsync(prestamos.PersonaId);
            persona.Balance += prestamos.Monto;
            
            int cantidad = await contextoP.SaveChangesAsync();
            
            return cantidad > 0;
          }

          public async Task<bool> Modificar(Prestamos prestamoActual)
          {
               var prestamoAnterior = await contextoP.Prestamos
                    .Where(p => p.PrestamoId == prestamoActual.PrestamoId)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();

               var personaAnterior = await contextoP.Personas.FindAsync(prestamoActual.PersonaId);
               personaAnterior.Balance -= prestamoActual.Monto;

               contextoP.Entry(prestamoActual).State = EntityState.Modified;
               
               var persona = await contextoP.Personas.FindAsync(prestamoActual.PersonaId);
               persona.Balance += prestamoActual.Monto;

               return await contextoP.SaveChangesAsync() > 0;
          }

          public async Task<bool> Editar(Prestamos prestamos)
          {
               if (await Existe(prestamos.PrestamoId))
                    return await this.Modificar(prestamos);
               else
                    return await this.Insertar(prestamos);
          }

          public async Task<Prestamos?> Buscar(int PrestamoId)
          {
               return await contextoP.Prestamos
                    .Where(o => o.PrestamoId == PrestamoId)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();
          }
          public async Task<List<Prestamos>> GetPrestamos(Expression<Func<Prestamos, bool>> Criterio)
          {
               return await contextoP.Prestamos
                   .AsNoTracking()
                   .Where(Criterio)
                   .ToListAsync();
          }
     }
}