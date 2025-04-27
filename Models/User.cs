using System.ComponentModel.DataAnnotations;

namespace LoginBackend.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; } // required en lugar de inicialización manual

        [Required]
        public required string LastName { get; set; } // required

        [Required]
        public required DateTime DateOfBirth { get; set; } // required

        [Required]
        public required string Phone { get; set; } // required

        [Required]
        [EmailAddress]
        public required string Email { get; set; } // required

        [Required]
        [MinLength(6)]
        public required string Password { get; set; } // required

        public int State { get; set; } // Estado del usuario
    }
}
