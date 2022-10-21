using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RegistroPagos.Data;
using RegistroPagos.Models;

namespace RegistroPagos.BLL
{
     public class PagosBLL
     {
          private Contexto contexto_;
          public PagosBLL(Contexto contexto)
          {
               contexto_ = contexto;
          }

          public bool Existe(int Id)
          {
               bool paso = false;

               try
               {
                    paso = contexto_.Pagos.Any(o => o.PagoId == Id);
               }
               catch (Exception)
               {
                    throw;
               }

               return paso;
          }

          public bool Guardar(Pagos pagos)
          {
               if (!Existe(pagos.PagoId))
                    return this.Insertar(pagos);
               else
                    return this.Modificar(pagos);
          }

          public bool Eliminar(int Id)
          {
               bool paso = false;

               try
               {
                    var pago = contexto_.Pagos.Find(Id);

                    if (pago != null)
                    {
                    contexto_.Pagos.Remove(pago);
                    paso = contexto_.SaveChanges() > 0;
                    }
               }
               catch (Exception)
               {
                    throw;
               }

               return paso;
          }
          public bool Insertar(Pagos pagos)
          {
               bool paso = false;

               try
               {  
                    if (contexto_.Pagos.Add(pagos) != null)
                         paso = contexto_.SaveChanges() > 0;
               }
               catch (Exception)
               {
                    throw;
               }

               return paso;
          }

          public bool Modificar(Pagos pagos)
          {
               bool paso = false;

               try
               {
                    contexto_.Database.ExecuteSqlRaw($"DELETE FROM PagosDetalles WHERE PrestamoId={pagos.PagoId}");

                    foreach (var Anterior in pagos.PagosDetalles)
                    {
                    contexto_.Entry(Anterior).State = EntityState.Added;
                    }

                    contexto_.Entry(pagos).State = EntityState.Modified;

                    paso = contexto_.SaveChanges() > 0;
               }
               catch (Exception)
               {
                    throw;
               }
               return paso;
          }

          public bool Editar(Pagos pagos)
          {
               if (Existe(pagos.PagoId))
                    return this.Modificar(pagos);
               else
                    return this.Insertar(pagos);
          }

          public Pagos? Buscar(int Id)
          {
               Pagos pago;
               
               try
               {
                    pago = contexto_.Pagos.Include(x => x.PagosDetalles).Where(c => c.PagoId == Id).SingleOrDefault();
               }
               catch (Exception)
               {
                    throw;
               }

               return pago;
          }
          public List<Pagos> GetPagos(Expression<Func<Pagos, bool>> Criterio)
          {
               List<Pagos> lista = new List<Pagos>();

               try
               {
                    lista = contexto_.Pagos.Where(Criterio).ToList();
               }
               catch (Exception)
               {
                    throw;
               }

               return lista;
          }

          public List<Prestamos> GetPrestamos(Expression<Func<Prestamos, bool>> Criterio)
          {
               return contexto_.Prestamos
                   .AsNoTracking()
                   .Where(Criterio)
                   .ToList();
          }
     }
}