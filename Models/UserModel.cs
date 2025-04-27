using System.ComponentModel.DataAnnotations;

public class UserModel
{
    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [Required]
    [StringLength(50)]
    public string LastName { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [StringLength(20)]
    public string Phone { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(50)]
    public string Email { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 6)]
    public string Password { get; set; }
}
