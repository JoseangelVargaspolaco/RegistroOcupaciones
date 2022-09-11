using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RegistroOcupacion.DAL;
using RegistroOcupacion.Entidades;

namespace RegistroOcupacion.BLL
{
     public class PersonaBLL
     {
          private Contexto contextos;

          public PersonaBLL(Contexto contexto)
          {
               contextos = contexto;
          }

          public bool Existe1(int PersonaId)
          {
               return contextos.Persona.Any(o => o.PersonaId == PersonaId);
          }

          private bool Insertar1(Persona persona)
          {
               contextos.Persona.Add(persona);
               return contextos.SaveChanges() > 0;
          }

          private bool Modificar1(Persona persona)
          {
               contextos.Entry(persona).State = EntityState.Modified;
               return contextos.SaveChanges() > 0;
          }

          public bool Guardar1(Persona persona)
          {
               if (!Existe1(persona.PersonaId))
                    return this.Insertar1(persona);
               else
                    return this.Modificar1(persona);
          }

          public bool Eliminar1(Persona persona)
          {
               contextos.Entry(persona).State = EntityState.Deleted;
               return contextos.SaveChanges() > 0;
          }

          public Persona? Buscar1(int personaId)
          {
               return contextos.Persona
                       .Where(o => o.PersonaId == personaId)
                       .AsNoTracking()
                       .SingleOrDefault();

          }

          public bool Editar(Persona persona)
          {
               if (!Existe1(persona.PersonaId))
                    return this.Insertar1(persona);
               else
                    return this.Modificar1(persona);
          }
          public List<Persona> GetList(Expression<Func<Persona, bool>> Criterio)
          {
               return contextos.Persona
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

     }
}