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

          public async Task<bool> Existe(int pagosId)
          {
               return await contexto_.Pagos.AnyAsync(o => o.PagoId == pagosId);
          }
          private async Task <bool> Insertar(Pagos pago)
          {
               contexto_.Pagos.Add(pago);
               
               foreach (var item in pago.PagosDetalles)
               {
                    var prestamo = await contexto_.Prestamos.FindAsync(item.PrestamoId);
                    prestamo!.Balance -= item.ValorPagado;

               }
               var persona =  contexto_.Personas.Find(pago.PersonaId);
               persona!.Balance -= pago.Monto;

               var insertados = contexto_.SaveChanges();

               return insertados > 0;

          }
          private async Task<bool> Modificar(Pagos pagoActual)
          {
               var pagoAnterior = contexto_.Pagos
                 .Where(p => p.PagoId == pagoActual.PagoId)
                 .AsNoTracking()
                 .SingleOrDefault();

               var Persona = contexto_.Personas.Find(pagoAnterior!.PersonaId);
               Persona!.Balance += pagoAnterior.Monto;

               foreach (var item in pagoAnterior.PagosDetalles)
               {
                    var prestamos =  contexto_.Prestamos.Find(item.PrestamoId);
                    prestamos!.Balance += item.ValorPagado;

               }

               await contexto_.Database.ExecuteSqlRawAsync($"Delete FROM PagosDetalle Where PagoId = {pagoActual.PagoId}");

               foreach (var item in pagoActual.PagosDetalles)
               {
                    contexto_.Entry(item).State = EntityState.Added;

                    var prestamo = contexto_.Prestamos.Find(item.PrestamoId);
                    prestamo!.Balance -= item.ValorPagado;
               }

               Persona.Balance -= pagoActual.Monto;

               contexto_.Entry(pagoActual).State = EntityState.Modified;

               contexto_.Entry(pagoActual).State = EntityState.Detached;

               return contexto_.SaveChanges() > 0;

          }
          public async Task<bool> Guardar(Pagos pagos)
          {
               if (!await Existe(pagos.PagoId))
                    return await this.Insertar(pagos);
               else
                    return await this.Modificar(pagos);
          }
          public async Task<bool>  Editar(Pagos pagos)
          {

               if (!await Existe(pagos.PagoId))
                    return await this.Insertar(pagos);
               else
                    return await this.Modificar(pagos);
          }

          public async Task<bool> Eliminar(Pagos pagos)
          {
               var persona = await contexto_.Personas.FindAsync(pagos.PersonaId);
               persona!.Balance += pagos.Monto;

               foreach (var count in pagos.PagosDetalles)
               {
                    var prestamo = await contexto_.Prestamos.FindAsync(count.PrestamoId);
                    prestamo!.Balance += count .ValorPagado;
               }

               contexto_.Entry(pagos).State = EntityState.Deleted;

               return await contexto_.SaveChangesAsync() > 0;
          }
          public async Task<Pagos?> Buscar(int pagoId)
          {
               return await contexto_.Pagos
                    .Where(o => o.PagoId == pagoId)
                    .Include(o => o.PagosDetalles)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();
          }

          public async Task<List<Pagos>> GetPagos(Expression<Func<Pagos, bool>> Criterio)
          {
               return await contexto_.Pagos
                    .Where(Criterio)
                    .AsNoTracking()
                    .ToListAsync();
          }
     }
}