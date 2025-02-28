using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthAPI.Core.Dtos;

public class GetUserDto
{
    public GetUserDto(int id, string? name, string? role, string? email)
    {
        Id = id;
        Name = name;
        Role = role;
        Email = email;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }
    [Required]
    public string? Name { get; private set; }
    [Required]
    public string? Role { get; private set; }

    [Required, EmailAddress]
    public string? Email { get; private set; }
}