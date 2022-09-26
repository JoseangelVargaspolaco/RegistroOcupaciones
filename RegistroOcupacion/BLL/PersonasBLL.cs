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
               return contextos.SaveChanges() > 0;
          }

          private bool Modificar(Personas persona)
          {
               contextos.Entry(persona).State = EntityState.Modified;
               return contextos.SaveChanges() > 0;
          }

          public bool Guardar(Personas persona)
          {
               if (!Existe(persona.PersonaId))
                    return this.Insertar(persona);
               else
                    return this.Modificar(persona);
          }

          public bool Eliminar(Personas persona)
          {
               contextos.Entry(persona).State = EntityState.Deleted;
               return contextos.SaveChanges() > 0;
          }

          public Personas? Buscar(int personaId)
          {
               return contextos.Personas
                       .Where(o => o.PersonaId == personaId)
                       .AsNoTracking()
                       .SingleOrDefault();

          }

          public bool Editar(Personas persona)
          {
               if (!Existe(persona.PersonaId))
                    return this.Insertar(persona);
               else
                    return this.Modificar(persona);
          }
          public List<Personas> GetPersonas(Expression<Func<Personas, bool>> Criterio)
          {
               return contextos.Personas
                   .AsNoTracking()
                   .Where(Criterio)
                   .ToList();
          }
          public List<Ocupaciones> GetOcupaciones(Expression<Func<Ocupaciones, bool>> Criterio){
            return contextos.Ocupaciones
                .AsNoTracking()
                .Where(Criterio)
                .ToList();
          }

          public List<Prestamos> GetPrestamos(Expression<Func<Prestamos, bool>> Criterio)
          {
               return contextos.Prestamos
                   .AsNoTracking()
                   .Where(Criterio)
                   .ToList();
          }
     }
}