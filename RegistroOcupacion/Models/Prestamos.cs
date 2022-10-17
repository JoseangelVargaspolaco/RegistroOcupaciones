using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroOcupacion.Models
{
   public class Prestamos
   {
      [Key]

      [Required(ErrorMessage = "El PrestamoId es requerido")]
      public int PrestamoId { get; set; }

      [Required(ErrorMessage = "Favor de Ingresar La Fecha de inicio")]
      public DateTime Fecha { get; set; }

      [Required(ErrorMessage = "Favor de Ingresar la fecha de vencimiento")]
      public DateTime Vence { get; set; }

      [Range(300, 200000000)]
      [Required(ErrorMessage = "Error, Ingrese el monto en el rango (300 hasta 200000000)")]
      public double Monto { get; set; }

      [Required(ErrorMessage = "Favor Selecccionar un PersonaId")]
      public int PersonaId { get; set; }

      [Required(ErrorMessage = "Favor de Ingresar el concepto")]
      public string? Concepto { get; set; }

      [Range(300, 200000000)]
      [Required(ErrorMessage = "Error, Ingrese el balance en el rego (300 hasta 200000000)")]
      public double Balance { get; set; }
   }
}