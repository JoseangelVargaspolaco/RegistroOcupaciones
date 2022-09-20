using System.ComponentModel.DataAnnotations;

namespace RegistroOcupacion.Models
{
     public class Prestamos
     {
          [Key]

          public int PrestamoId { get; set; }

          [Required(ErrorMessage = "La fecha es requerida")]
          public  DateTime Fecha { get; set; }= DateTime.Now;

          [Required(ErrorMessage = "La fecha de vencimiento es requerida")]
         
          public  DateTime Vence { get; set; }= DateTime.Now;

          public int PersonaId { get; set; }

          public string? Consepto { get; set; }

          public double Monto { get; set; }

          public double Balance { get; set; } = 10000;

     }
}