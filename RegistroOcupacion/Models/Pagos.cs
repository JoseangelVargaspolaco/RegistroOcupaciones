using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroOcupacion.Models
{
     public class Pagos
     {
        [Key]

        [Required(ErrorMessage = "El PagoId es requerido")]
        public int PagoId { get; set; }

        [Required(ErrorMessage = "El Fecha es requerida")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El PersonaId es requerido")]
        public int PersonaId { get; set; }

        [Required(ErrorMessage = "El Concepto es requerido")]
        public string? Concepto { get; set; }

        [Required(ErrorMessage = "El Monto es requerido")]
        public float? Monto { get; set; }

        /*Creando instancia del PagosDetalles

        [ForeignKey("PagoId")]
        public virtual List<PagosDetalles> PagosDetalles { get; set; } = new List<PagosDetalles>();*/
    }
}