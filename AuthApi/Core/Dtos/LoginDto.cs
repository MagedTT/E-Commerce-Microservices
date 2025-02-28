using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Core.Dtos;

public class LoginDto
{
    public LoginDto(string? email, string? password)
    {
        Email = email;
        Password = password;
    }

    [Required, EmailAddress]
    public string? Email { get; private set; }
    [Required]
    public string? Password { get; private set; }
}
