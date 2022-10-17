using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RegistroOcupacion.DAL;
using RegistroOcupacion.Models;

namespace RegistroOcupacion.BLL
{
     public class PersonasBLL
     {
          private Contexto contextos;
          public PersonasBLL(Contexto contexto)
          {
               contextos = contexto;
          }

          public bool Existe(int PersonaId)
          {
               return contextos.Personas.Any(o => o.PersonaId == PersonaId);
          }

          private bool Insertar(Personas personas)
          {
            contextos.Personas.Add(personas);
            int cantidad = contextos.SaveChanges();
            return cantidad > 0;
          }

          private bool Modificar(Personas personas)
          {
               contextos.Entry(personas).State = EntityState.Modified;
               return contextos.SaveChanges() > 0;
          }

          public bool Guardar(Personas personas)
          {
               if (!Existe(personas.PersonaId))
                    return this.Insertar(personas);
               else
                    return this.Modificar(personas);
          }

          public bool Eliminar(Personas personas)
          {
               contextos.Entry(personas).State = EntityState.Deleted;
               return contextos.SaveChanges() > 0;
          }

          public Personas? Buscar(int personaId)
          {
               return contextos.Personas
                    .Where(o => o.PersonaId == personaId)
                    .AsNoTracking()
                    .SingleOrDefault();

          }

          public bool Editar(Personas personas)
          {
               if (Existe(personas.PersonaId))
                    return this.Modificar(personas);
               else
                    return this.Insertar(personas);
          }
          public List<Personas> GetPersonas(Expression<Func<Personas, bool>> Criterio)
          {
               return contextos.Personas
                   .AsNoTracking()
                   .Where(Criterio)
                   .ToList();
          }
     }
}