using System.ComponentModel.DataAnnotations;

namespace RegistroOcupacion.Models
{
     public class PagosDetalles
     {
          [Key]

          [Required(ErrorMessage = "El Id es requerido")]
          public int Id { get; set; }

          [Required(ErrorMessage = "El PagoId es requerido")]
          public int PagoId { get; set; }

          [Required(ErrorMessage = "El PrestamoId es requerido")]
          public int PrestamoId { get; set; }

          [Required(ErrorMessage = "El ValorPagado es requerido")]
          public float? ValorPagado { get; set; }
     }
}