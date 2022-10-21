using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RegistroPagos.Data;
using RegistroPagos.Models;

namespace RegistroPagos.BLL
{
     public class PrestamosBLL
     {
          private Contexto contextoP;
          public PrestamosBLL(Contexto contexto)
          {
               contextoP = contexto;
          }

          public bool Existe(int prestamosId)
          {
               return contextoP.Prestamos.Any(o => o.PrestamoId == prestamosId);
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
            
            var persona = contextoP.Personas.Find(prestamos.PersonaId);
            persona.Balance += prestamos.Monto;
            
            int cantidad = contextoP.SaveChanges();
            
            return cantidad > 0;
          }

          public bool Modificar(Prestamos prestamoActual)
          {
               var prestamoAnterior = contextoP.Prestamos
                    .Where(p => p.PrestamoId == prestamoActual.PrestamoId)
                    .AsNoTracking()
                    .SingleOrDefault();

               var personaAnterior = contextoP.Personas.Find(prestamoActual.PersonaId);
               personaAnterior.Balance -= prestamoActual.Monto;

               contextoP.Entry(prestamoActual).State = EntityState.Modified;
               
               var persona = contextoP.Personas.Find(prestamoActual.PersonaId);
               persona.Balance += prestamoActual.Monto;

               return contextoP.SaveChanges() > 0;
          }

          public bool Editar(Prestamos prestamos)
          {
               if (Existe(prestamos.PrestamoId))
                    return this.Modificar(prestamos);
               else
                    return this.Insertar(prestamos);
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
     }
}