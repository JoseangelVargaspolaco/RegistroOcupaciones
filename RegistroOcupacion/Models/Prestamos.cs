using System.ComponentModel.DataAnnotations;

namespace RegistroOcupacion.Models
{
     public class Prestamos
     {
          [Key]

          public int PrestamoId { get; set; }

          public  DateTime Fecha { get; set; }= DateTime.Now;
         
          public  DateTime Vence { get; set; }= DateTime.Now;

          public int PersonaId { get; set; }

          public string? Consepto { get; set; }

          public double Monto { get; set; }

          public double Balance { get; set; } = 10000;

     }
}