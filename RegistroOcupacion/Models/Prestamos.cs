using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroOcupacion.Models
{
   public class Prestamos
   {
      [Key]

      [Required(ErrorMessage = "El PrestamoId es requerido")]
      public int PrestamoId { get; set; }

      [Required(ErrorMessage = "Favor de Ingresar La Fecha de inicio.")]
      public DateTime Fecha { get; set; }

      [Required(ErrorMessage = "Favor de Ingresar la fecha de vencimiento")]
      public DateTime Vence { get; set; }

      [Range(100, 200000000)]
      [Required(ErrorMessage = "Favor de ingresar el monto")]
      public float? Monto { get; set; }

      [Required(ErrorMessage = "Favor Selecccionar una pesona")]
      public int PersonaId { get; set; }

      [Required(ErrorMessage = "Favor de Ingresar eL concepto")]
      public string? Concepto { get; set; }
      [Required(ErrorMessage = "El  balance es requerido")]
      public float? Balance { get; set; }
   }
}