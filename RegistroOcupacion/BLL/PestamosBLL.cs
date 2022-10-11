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

        public bool Modificar(Prestamos prestamos)
        {
            //descontar el monto anterior
            var prestamoAnterior = contextoP.Prestamos
                .Where(p => p.PrestamoId == prestamos.PrestamoId)
                .AsNoTracking()
                .SingleOrDefault();

            var personas = contextoP.Personas.Find(prestamos.PersonaId);
            personas.Balance -= prestamos.Monto;

            contextoP.Entry(prestamos).State = EntityState.Modified;
            
            //descontar el monto nuevo
            var persona = contextoP.Personas.Find(prestamos.PersonaId);
            persona.Balance += prestamos.Monto;

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

     }
}