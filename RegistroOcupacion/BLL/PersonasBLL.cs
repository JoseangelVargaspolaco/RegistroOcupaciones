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
               return contextos.Persona.Any(o => o.PersonaId == PersonaId);
          }

          private bool Insertar(Persona persona)
          {
               contextos.Persona.Add(persona);
               return contextos.SaveChanges() > 0;
          }

          private bool Modificar(Persona persona)
          {
               contextos.Entry(persona).State = EntityState.Modified;
               return contextos.SaveChanges() > 0;
          }

          public bool Guardar(Persona persona)
          {
               if (!Existe(persona.PersonaId))
                    return this.Insertar(persona);
               else
                    return this.Modificar(persona);
          }

          public bool Eliminar(Persona persona)
          {
               contextos.Entry(persona).State = EntityState.Deleted;
               return contextos.SaveChanges() > 0;
          }

          public Persona? Buscar(int personaId)
          {
               return contextos.Persona
                       .Where(o => o.PersonaId == personaId)
                       .AsNoTracking()
                       .SingleOrDefault();

          }

          public bool Editar(Persona persona)
          {
               if (!Existe(persona.PersonaId))
                    return this.Insertar(persona);
               else
                    return this.Modificar(persona);
          }
          public List<Persona> GetPersonas(Expression<Func<Persona, bool>> Criterio)
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

          public List<Prestamos> GetPrestamos(Expression<Func<Prestamos, bool>> Criterio)
          {
               return contextos.Prestamos
                   .AsNoTracking()
                   .Where(Criterio)
                   .ToList();
          }
     }
}