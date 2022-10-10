using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RegistroOcupacion.DAL;
using RegistroOcupacion.Models;

namespace RegistroOcupacion.BLL
{
   public class PagosDetallesBLL
   {
      private Contexto contexto_;
      public PagosDetallesBLL(Contexto contexto)
      {
         contexto_ = contexto;
      }

      public bool Existe(int Id)
      {
         return contexto_.PagosDetalles.Any(o => o.Id == Id);
      }

      public bool Guardar(PagosDetalles pagosDetalles)
      {
         if (!Existe(pagosDetalles.Id))
            return this.Insertar(pagosDetalles);
         else
            return this.Modificar(pagosDetalles);
      }

      public bool Eliminar(PagosDetalles pagosDetalles)
      {
         contexto_.Entry(pagosDetalles).State = EntityState.Deleted;
         return contexto_.SaveChanges() > 0;
      }
      public bool Insertar(PagosDetalles pagosDetalles)
      {
         contexto_.PagosDetalles.Add(pagosDetalles);
         return contexto_.SaveChanges() > 0;
      }

      public bool Modificar(PagosDetalles pagosDetalles)
      {
         contexto_.Entry(pagosDetalles).State = EntityState.Modified;
         return contexto_.SaveChanges() > 0;
      }

      public bool Editar(PagosDetalles pagosDetalles)
      {
         if (!Existe(pagosDetalles.Id))
            return this.Insertar(pagosDetalles);
         else
            return this.Modificar(pagosDetalles);
      }

      public PagosDetalles? Buscar(int Id)
      {
         return contexto_.PagosDetalles
            .Where(o => o.Id == Id)
            .AsNoTracking()
            .SingleOrDefault();
      }

      public List<PagosDetalles> GetPagosDetalles(Expression<Func<PagosDetalles, bool>> Criterio)
      {
         return contexto_.PagosDetalles
            .AsNoTracking()
            .Where(Criterio)
            .ToList();
      }

      public List<PagosDetalles> GetPagosDetalle(Expression<Func<PagosDetalles, bool>> Criterio)
      {
         return contexto_.PagosDetalles
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