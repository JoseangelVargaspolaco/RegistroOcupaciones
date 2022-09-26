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

          private bool Insertar(Prestamos prestamos)
          {
               contextoP.Prestamos.Add(prestamos);
               return contextoP.SaveChanges() > 0;
          }

          private bool Modificar(Prestamos prestamos)
          {
               contextoP.Entry(prestamos).State = EntityState.Modified;
               return contextoP.SaveChanges() > 0;
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
               contextoP.Entry(prestamos).State = EntityState.Deleted;
               return contextoP.SaveChanges() > 0;
          }

          public Prestamos? Buscar(int PrestamoId)
          {
               return contextoP.Prestamos
                       .Where(o => o.PrestamoId == PrestamoId)
                       .AsNoTracking()
                       .SingleOrDefault();

          }

          public bool Editar(Prestamos prestamos)
          {
               if (!Existe(prestamos.PrestamoId))
                    return this.Insertar(prestamos);
               else
                    return this.Modificar(prestamos);
          }
          public List<Prestamos> GetPrestamos(Expression<Func<Prestamos, bool>> Criterio)
          {
               return contextoP.Prestamos
                   .AsNoTracking()
                   .Where(Criterio)
                   .ToList();
          }
          public List<Personas>GetPersonas(Expression<Func<Personas, bool>> Criterio)
          {
               return contextoP.Personas
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