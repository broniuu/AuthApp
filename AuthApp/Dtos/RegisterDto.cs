using System.ComponentModel.DataAnnotations;

namespace AuthApp.Dtos;

public class RegisterDto
{ 
    [Required] public string Login { get; set; }
    [Required] public string Email { get; set; }

    [Required]
    [StringLength(8,MinimumLength = 4)]
    public string Password { get; set; }

}