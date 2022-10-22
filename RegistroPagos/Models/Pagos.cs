using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroPagos.Models
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

        [Range(300, 200000000)]
        [Required(ErrorMessage = "Error, Ingrese el monto en el rango (300 hasta 200000000)")]
        public float Monto { get; set; }

        //Creando llave foranea del PagosDetalles

        [ForeignKey("PagoId")]
        public virtual List<PagosDetalles> Detalle { get; set; } = new List<PagosDetalles>();
    }
}