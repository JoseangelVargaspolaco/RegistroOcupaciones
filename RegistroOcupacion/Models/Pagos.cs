using System.ComponentModel.DataAnnotations;

namespace RegistroOcupacion.Models
{
     public class Pagos
     {
         [Key]
         public int PagoId { get; set; }

         public DateTime Fecha { get; set; }

         public int PersonaId { get; set; }

         public string? Concepto { get; set; }

         public double Monto { get; set; }
         
     }
}