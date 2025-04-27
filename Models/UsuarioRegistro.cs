using System;
using System.ComponentModel.DataAnnotations;

namespace LoginBackend.Models
{
    public class UsuarioRegistro
    {
        public int UsuId { get; set; } // Añadir este campo si realmente lo necesitas
        public int RegId { get; set; } // Añadir este campo si realmente lo necesitas

        // Campos de tbl_registro
        [Required]
        [StringLength(50)]
        public string? RegNombre { get; set; } // reg_nombre

        [Required]
        [StringLength(50)]
        public string? RegApellido { get; set; } // reg_apellido

        [Required]
        public DateTime RegFechaNacim { get; set; } // reg_fechaNacim

        [Required]
        [StringLength(20)]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "El teléfono debe tener 10 dígitos.")]
        public string? RegTelefono { get; set; } // reg_telefono

        public int RegEstado { get; set; } = 1; // reg_estado

        // Campos de tbl_usuarios
        [Required]
        [StringLength(50)]
        public string? UsuUsuario { get; set; } // usu_usuario

        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string? UsuCorreo { get; set; } // usu_correo

        [Required]
        [StringLength(50)]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string? UsuContrasenia { get; set; } // usu_contrasenia

        public int UsuEstado { get; set; } = 1; // usu_estado
    }
}
