using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderAPI.Core.Dtos;

public class UserDto
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Role { get; set; }
    [Required]
    public string? Password { get; set; }
    [Required, EmailAddress]
    public string? Email { get; set; }
}