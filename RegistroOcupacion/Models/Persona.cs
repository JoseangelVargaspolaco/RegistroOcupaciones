using System.ComponentModel.DataAnnotations;

namespace RegistroOcupacion.Models
{
     public class Persona
     {
          [Key]

          
          [Range(minimum: 1, maximum: 2000000000, ErrorMessage = "El ID no puede ser menor a 1")]
          public int PersonaId { get; set; }

          [Required(ErrorMessage = "La nombre de la persona es requerido")]
          public string? Nombre { get; set; }

          [Required(ErrorMessage = "El numero de telefono es requerido")]
          public double Telefono { get; set; }

          [Required(ErrorMessage = "El numero de celular es requerido")]
          public double Celular { get; set; }

          [Required(ErrorMessage = "El email es requerido")]
          public string? Email { get; set; }

          [Required(ErrorMessage = "El numero de celular es requerido")]
          public string? Direccion { get; set; }

          public  DateTime FechaNacimiento { get; set; } = DateTime.Now;

          [Required(ErrorMessage = "El ID de una ocupacion es requerido")]
          public int OcupacionId { get; set; }

     }
}