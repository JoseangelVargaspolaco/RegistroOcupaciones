using System.ComponentModel.DataAnnotations;

namespace RegistroOcupacion.Models
{
     public class Ocupaciones
     {

          [Key]

          [Range(minimum: 1, maximum: 2000, ErrorMessage = "El ID no puede ser menor a 1")]
          public int OcupacionId { get; set; }

          [Required(ErrorMessage ="La descripcion es requerida")]
          public string? Descripcion { get; set; }

          [Range(minimum: 100, maximum: 2000000, ErrorMessage = "El salario no esta dentro del rango requerido ( entre 100 y 2,000,000)")]

          public float Salario { get; set; }
     }
}
