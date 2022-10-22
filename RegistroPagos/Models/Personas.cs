using System.ComponentModel.DataAnnotations;

namespace RegistroPagos.Models
{
     public class Personas
     {
          [Key]

          [Required(ErrorMessage = "El  PersonaId es requerido")]
          public int PersonaId { get; set; }

          [Required(ErrorMessage = "La nombre de la persona es requerido")]
          public string? Nombres { get; set; }

          [RegularExpression(@"^\d{3}-\d{3}-\d{4}$"), Phone(ErrorMessage = "El formato de numero es incorrecto")]
          public string? Telefono { get; set; }

     
          [RegularExpression(@"^\d{3}-\d{3}-\d{4}$"), Phone(ErrorMessage = "Favor de ingresar correctamente el numero celular.")]
          
          public string? Celular { get; set; }

          [RegularExpression(@"^\d{3}-\d{3}-\d{4}$"), EmailAddress(ErrorMessage = "El numero de celular es requerido")]
          public string? Email { get; set; }

          [Required(ErrorMessage = "La direccion es requerida")]
          public string? Direccion { get; set; }
          public DateTime FechaNacimiento { get; set; }

          [Required(ErrorMessage = "El ID de una ocupacion es requerido")]
          public int OcupacionId { get; set; }
          public double Balance { get; set; }

     }
}