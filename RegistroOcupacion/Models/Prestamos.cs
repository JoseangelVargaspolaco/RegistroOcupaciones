using System.ComponentModel.DataAnnotations;

namespace RegistroOcupacion.Models
{
     public class Prestamos
     {
          [Key]

          [Range(minimum: 1, maximum: 2000000000, ErrorMessage = "El ID no puede ser menor a 1")] 
          public int PrestamoId { get; set; }

          public  DateTime Fecha { get; set; }= DateTime.Now;
         
          public  DateTime Vence { get; set; }= DateTime.Now;

          public int PersonaId { get; set; }

          [Required(ErrorMessage = "El concepto es requerido")]
          public string? Concepto { get; set; }

          [Range(minimum: 10000, maximum: 2000000000, ErrorMessage = "El monto no esta dentro del rango requerido (entre 100 y 2,000,000)")]
          public double Monto { get; set; }

          public double Balance { get; set; } = 1000000;

     }
}