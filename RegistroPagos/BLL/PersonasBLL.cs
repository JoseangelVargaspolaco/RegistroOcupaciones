using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RegistroPagos.Data;
using RegistroPagos.Models;

namespace RegistroPagos.BLL
{
     public class PersonasBLL
     {
          private Contexto contextos;
          public PersonasBLL(Contexto contexto)
          {
               contextos = contexto;
          }

          public async Task<bool> Existe(int PersonaId)
          {
               return await contextos.Personas.AnyAsync(o => o.PersonaId == PersonaId);
          }

          private async Task<bool> Insertar(Personas personas)
          {
            contextos.Personas.Add(personas);
            int cantidad = await contextos.SaveChangesAsync();
            return cantidad > 0;
          }

          private async Task<bool> Modificar(Personas personas)
          {
               contextos.Entry(personas).State = EntityState.Modified;
               return await contextos.SaveChangesAsync() > 0;
          }

          public async Task<bool> Guardar(Personas personas)
          {
               if (! await Existe(personas.PersonaId))
                    return await this.Insertar(personas);
               else
                    return await this.Modificar(personas);
          }

          public async Task<bool> Eliminar(Personas personas)
          {
               contextos.Entry(personas).State = EntityState.Deleted;
               return await contextos.SaveChangesAsync() > 0;
          }


          public async Task<bool> Editar(Personas personas)
          {
               if(!await Existe(personas.PersonaId))
                    return await this.Insertar(personas);
               else
                    return await this.Modificar(personas);
          }

          public async Task<Personas?> Buscar(int personaId)
          {
               return await contextos.Personas
                    .Where(o => o.PersonaId == personaId)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();

          }
          public async Task<List<Personas>> GetPersonas(Expression<Func<Personas, bool>> Criterio)
          {
               return await contextos.Personas
                   .AsNoTracking()
                   .Where(Criterio)
                   .ToListAsync();
          }
     }
}