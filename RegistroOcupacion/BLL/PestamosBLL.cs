using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RegistroOcupacion.DAL;
using RegistroOcupacion.Models;

namespace RegistroOcupacion.BLL
{
     public class PrestamosBLL
     {
          private Contexto contextoP;
          public PrestamosBLL(Contexto contexto)
          {
               contextoP = contexto;
          }

          public bool Existe(int PrestamosId)
          {
               return contextoP.Prestamos.Any(o => o.PrestamoId == PrestamosId);
          }

          public bool Guardar(Prestamos prestamos)
          {
               if (!Existe(prestamos.PrestamoId))
                    return this.Insertar(prestamos);
               else
                    return this.Modificar(prestamos);
          }

          public bool Eliminar(Prestamos prestamos)
          {
            var personas = contextoP.Personas.Find(prestamos.PersonaId);
            personas.Balance -= prestamos.Monto;

            contextoP.Entry(prestamos).State = EntityState.Deleted;
            return contextoP.SaveChanges() > 0;
          }
          public bool Insertar(Prestamos prestamos)
          {
            contextoP.Prestamos.Add(prestamos);
            
            var personas = contextoP.Personas.Find(prestamos.PersonaId);
            personas.Balance += prestamos.Monto;
            
            int cantidad = contextoP.SaveChanges();
            
            return cantidad > 0;
          }

          public bool Modificar(Prestamos prestamo_actual)
          {
            var prestamo_anterior = contextoP.Prestamos
                .Where(p => p.PrestamoId == prestamo_actual.PrestamoId)
                .AsNoTracking()
                .SingleOrDefault();

            var persona_anterior = contextoP.Personas.Find(prestamo_anterior.PersonaId);
            persona_anterior.Balance -= prestamo_anterior.Monto;

            contextoP.Entry(prestamo_actual).State = EntityState.Modified;
            
            var personas = contextoP.Personas.Find(prestamo_actual.PersonaId);
            personas.Balance += prestamo_actual.Monto;

            return contextoP.SaveChanges() > 0;
          }

          public bool Editar(Prestamos prestamos)
          {
               if (!Existe(prestamos.PrestamoId))
                    return this.Insertar(prestamos);
               else
                    return this.Modificar(prestamos);
          }

          public Prestamos? Buscar(int PrestamoId)
          {
               return contextoP.Prestamos
                       .Where(o => o.PrestamoId == PrestamoId)
                       .AsNoTracking()
                       .SingleOrDefault();
          }
          public List<Prestamos> GetPrestamos(Expression<Func<Prestamos, bool>> Criterio)
          {
               return contextoP.Prestamos
                   .AsNoTracking()
                   .Where(Criterio)
                   .ToList();
          }

          public List<Personas> GetPersonas(Expression<Func<Personas, bool>> Criterio)
          {
               return contextoP.Personas
                   .AsNoTracking()
                   .Where(Criterio)
                   .ToList();
          }
     }
}