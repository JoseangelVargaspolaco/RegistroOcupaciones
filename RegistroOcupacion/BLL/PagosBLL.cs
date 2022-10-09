using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RegistroOcupacion.DAL;
using RegistroOcupacion.Models;

namespace RegistroOcupacion.BLL
{
     public class PagosBLL
     {
          private Contexto contexto_;
          public PagosBLL(Contexto contexto)
          {
               contexto_ = contexto;
          }

          public bool Existe(int PagoId)
          {
               return contexto_.Pagos.Any(o => o.PagoId == PagoId);
          }

          public bool Guardar(Pagos pagos)
          {
               if (!Existe(pagos.PagoId))
                    return this.Insertar(pagos);
               else
                    return this.Modificar(pagos);
          }

          public bool Eliminar(Pagos pagos)
          {
            contexto_.Entry(pagos).State = EntityState.Deleted;
            return contexto_.SaveChanges() > 0;
          }
          public bool Insertar(Pagos pagos)
          {
             contexto_.Pagos.Add(pagos);
            
             var personas = contexto_.Personas.Find(pagos.PagoId);
             pagos.Monto += personas.Balance;
            
            int cantidad = contexto_.SaveChanges();
            
            return cantidad > 0;
          }

          public bool Modificar(Pagos pagos)
          {
              contexto_.Entry(pagos).State = EntityState.Modified;
              return contexto_.SaveChanges() > 0;
          }

          public bool Editar(Pagos pagos)
          {
               if (!Existe(pagos.PagoId))
                    return this.Insertar(pagos);
               else
                    return this.Modificar(pagos);
          }

          public Pagos? Buscar(int PagoId)
          {
               return contexto_.Pagos
                       .Where(o => o.PagoId == PagoId)
                       .AsNoTracking()
                       .SingleOrDefault();
          }

          public List<Pagos> GetPagos(Expression<Func<Pagos, bool>> Criterio)
          {
               return contexto_.Pagos
                   .AsNoTracking()
                   .Where(Criterio)
                   .ToList();
          }
          public List<Prestamos> GetPrestamos(Expression<Func<Prestamos, bool>> Criterio)
          {
               return contexto_.Prestamos
                   .AsNoTracking()
                   .Where(Criterio)
                   .ToList();
          }

          public List<Personas> GetPersonas(Expression<Func<Personas, bool>> Criterio)
          {
               return contexto_.Personas
                   .AsNoTracking()
                   .Where(Criterio)
                   .ToList();
          }
     }
}