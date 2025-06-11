using System;
using System.ComponentModel.DataAnnotations;

namespace Actividad3LengProg3.Models
{
    public class EstudianteViewModel
    {
        [Required(ErrorMessage = "El nombre completo es requerido")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
        [Display(Name = "Nombre Completo")]
        public required string NombreCompleto { get; set; }

        [Required(ErrorMessage = "La matrícula es requerida")]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "La matrícula debe tener entre 6 y 15 caracteres")]
        [Display(Name = "Matrícula")]
        public required string Matricula { get; set; }

        [Required(ErrorMessage = "La carrera es requerida")]
        [Display(Name = "Carrera")]
        public required string Carrera { get; set; }

        [Required(ErrorMessage = "El correo institucional es requerido")]
        [EmailAddress(ErrorMessage = "Formato de correo electrónico inválido")]
        [Display(Name = "Correo Institucional")]
        public required string CorreoInstitucional { get; set; }

        [Phone(ErrorMessage = "Formato de teléfono inválido")]
        [MinLength(10, ErrorMessage = "El teléfono debe tener al menos 10 dígitos")]
        [Display(Name = "Teléfono")]
        public required string Telefono { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es requerida")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El género es requerido")]
        [Display(Name = "Género")]
        public required string Genero { get; set; }

        [Required(ErrorMessage = "El turno es requerido")]
        [Display(Name = "Turno")]
        public required string Turno { get; set; }

        [Required(ErrorMessage = "El tipo de ingreso es requerido")]
        [Display(Name = "Tipo de Ingreso")]
        public required string TipoIngreso { get; set; }

        [Display(Name = "¿Está Becado?")]
        public bool EstaBecado { get; set; }

        [Range(0, 100, ErrorMessage = "El porcentaje debe estar entre 0 y 100")]
        [Display(Name = "Porcentaje de Beca")]
        public int? PorcentajeBeca { get; set; }

        [Range(typeof(bool), "true", "true", ErrorMessage = "Debe aceptar los términos y condiciones")]
        [Display(Name = "Acepto los términos y condiciones")]
        public bool AceptaTerminos { get; set; }
    }
}