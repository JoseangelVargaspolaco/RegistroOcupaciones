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

          public async Task<bool> Guardar(Pagos pagos)
          {
            var existe = await Existe(pagos.PagoId);

            if (!existe)
                return await this.Insertar(pagos);
            else
                return await this.Modificar(pagos);
          }

          private async Task<bool> Insertar(Pagos pago)
          {
            await contexto_.Pagos.AddAsync(pago);

            foreach (var item in pago.Detalle)
            {
                var prestamo = await contexto_.Prestamos.FindAsync(item.PrestamoId);
                prestamo!.Balance -= item.ValorPagado;
            }

            var persona = await contexto_.Personas.FindAsync(pago.PersonaId);
            persona!.Balance -= pago.Monto;

            var insertados = await contexto_.SaveChangesAsync();

            return insertados > 0;
          }
          private async Task<bool> Modificar(Pagos pagoActual)
          {
            var pagoAnterior = await contexto_.Pagos
                 .Where(p => p.PagoId == pagoActual.PagoId)
                 .AsNoTracking()
                 .SingleOrDefaultAsync();

            //revertir el balance pagado a la persona.
            var persona = await contexto_.Personas.FindAsync(pagoAnterior!.PersonaId);
            persona!.Balance += pagoAnterior.Monto;

            //revertir el balance pagado a los prestamos
            foreach (var item in pagoAnterior.Detalle)
            {
                var prestamos = await contexto_.Prestamos.FindAsync(item.PrestamoId);
                prestamos!.Balance += item.ValorPagado;
            }

            //borrar el detalle anterior
            await contexto_.Database.ExecuteSqlRawAsync($"Delete FROM PagosDetalle Where PagoId = {pagoActual.PagoId}");

            foreach (var item in pagoActual.Detalle)
            {
                contexto_.Entry(item).State = EntityState.Added;

                //afectar los prestamos segun el nuevo valor pagado.
                var prestamo = await contexto_.Prestamos.FindAsync(item.PrestamoId);
                prestamo!.Balance -= item.ValorPagado;
            }
            persona.Balance -= pagoActual.Monto;
            contexto_.Entry(pagoActual).State = EntityState.Modified;
            var cantidad = await contexto_.SaveChangesAsync();

            contexto_.Entry(pagoActual).State = EntityState.Detached;
            return cantidad > 0;
          }

          public async Task<bool> Editar(Pagos pagos)
          {
               contexto_.Entry(pagos).State = EntityState.Modified;
               return await contexto_.SaveChangesAsync() > 0;
          }
        
          public async Task<bool> Eliminar(Pagos pagos)
          {
            var persona = await contexto_.Personas.FindAsync(pagos.PersonaId);
            persona!.Balance += pagos.Monto;

            foreach (var item in pagos.Detalle)
            {
                var prestamos = await contexto_.Prestamos.FindAsync(item.PrestamoId);
                prestamos!.Balance += item.ValorPagado;
            }

            contexto_.Entry(pagos).State = EntityState.Deleted;

            var cantidad = await contexto_.SaveChangesAsync();
            
            return cantidad > 0;
          }
        
          public async Task<Pagos?> Buscar(int pagoId)
          {
            var pago = await contexto_.Pagos
            .Where(o => o.PagoId == pagoId)
            .Include(o => o.Detalle)
            .AsNoTracking()
            .SingleOrDefaultAsync();
            
            return pago;
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