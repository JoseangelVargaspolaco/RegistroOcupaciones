using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RegistroOcupacion.DAL;
using RegistroOcupacion.Entidades;

namespace RegistroOcupacion.BLL
{
     public class PrestamosBLL
     {
          private Contexto contextoP;
          public PrestamosBLL(Contexto contexto)
          {
               contextoP = contexto;
          }

          public bool Existe2(int PrestamosId)
          {
               return contextoP.Prestamos.Any(o => o.PrestamoId == PrestamosId);
          }

          private bool Insertar2(Prestamos prestamos)
          {
               contextoP.Prestamos.Add(prestamos);
               return contextoP.SaveChanges() > 0;
          }

          private bool Modificar2(Prestamos prestamos)
          {
               contextoP.Entry(prestamos).State = EntityState.Modified;
               return contextoP.SaveChanges() > 0;
          }

          public bool Guardar2(Prestamos prestamos)
          {
               if (!Existe2(prestamos.PrestamoId))
                    return this.Insertar2(prestamos);
               else
                    return this.Modificar2(prestamos);
          }

          public bool Eliminar2(Prestamos prestamos)
          {
               contextoP.Entry(prestamos).State = EntityState.Deleted;
               return contextoP.SaveChanges() > 0;
          }

          public Prestamos? Buscar2(int PrestamoId)
          {
               return contextoP.Prestamos
                       .Where(o => o.PrestamoId == PrestamoId)
                       .AsNoTracking()
                       .SingleOrDefault();

          }

          public bool Editar(Prestamos prestamos)
          {
               if (!Existe2(prestamos.PrestamoId))
                    return this.Insertar2(prestamos);
               else
                    return this.Modificar2(prestamos);
          }
           public List<Prestamos> GetPrestamos(Expression<Func<Prestamos, bool>> Criterio)
          {
               return contextoP.Prestamos
                   .AsNoTracking()
                   .Where(Criterio)
                   .ToList();
          }
          public List<Persona>GetPersonas(Expression<Func<Persona, bool>> Criterio)
          {
               return contextoP.Persona
                   .AsNoTracking()
                   .Where(Criterio)
                   .ToList();
          }
         public List<Ocupaciones> GetOcupaciones(Expression<Func<Ocupaciones, bool>> Criterio){
            return contextoP.Ocupaciones
                .AsNoTracking()
                .Where(Criterio)
                .ToList();
        }

     }
}