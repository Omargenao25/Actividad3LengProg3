using System;
using System.ComponentModel.DataAnnotations;

namespace Actividad3LengProg3.Models
{
    public class EstudianteViewModel
    {
        [Required]
        [StringLength(100)]
        public required string NombreCompleto { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 6)]
        public required string Matricula { get; set; }

        [Required]
        public required string Carrera { get; set; }

        [Required]
        [EmailAddress]
        public required string CorreoInstitucional { get; set; }

        [Phone]
        [MinLength(10)]
        public required string Telefono { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        public required string Genero { get; set; }

        [Required]
        public required string Turno { get; set; }

        [Required]
        public required string TipoIngreso { get; set; }

        public bool EstaBecado { get; set; }

        [Range(0, 100)]
        public int? PorcentajeBeca { get; set; }

        [Range(typeof(bool), "true", "true", ErrorMessage = "Debe aceptar los términos y condiciones.")]
        public bool TerminosCondiciones { get; set; }
    }
}
