using System.ComponentModel.DataAnnotations;

namespace RegistroOcupacion.Models
{
     public class Personas
     {
          [Key]

          [Required(ErrorMessage = "El  PersonaId es requerido")]
          public int PersonaId { get; set; }

          [Required(ErrorMessage = "La nombre de la persona es requerido")]
          public string? Nombres { get; set; }

          [Required(ErrorMessage = "El numero de telefono es requerido")]
          public string? Telefono { get; set; }

          [Required(ErrorMessage = "El numero de celular es requerido")]
          public string? Celular { get; set; }

          [Required(ErrorMessage = "El email es requerido")]
          public string? Email { get; set; }

          [Required(ErrorMessage = "La direccion es requerida")]
          public string? Direccion { get; set; }
          public DateTime FechaNacimiento { get; set; }

          [Required(ErrorMessage = "El ID de una ocupacion es requerido")]
          public int OcupacionId { get; set; }
          public double Balance { get; set; }

     }
}