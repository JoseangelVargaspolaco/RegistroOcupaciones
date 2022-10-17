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

          [Range(100, 200000000)]
          [Required(ErrorMessage = "Error, Ingrese un valorPagado en el rango (100 hasta 200000000)")]
          public double ValorPagado { get; set; }

          public PagosDetalles(int id, int prestamoId, double valorPagado)
          {
               PagoId = id;
               PrestamoId = prestamoId;
               ValorPagado = valorPagado;
          }
     }
}