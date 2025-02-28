namespace AuthAPI.Core.Models;

public class AppUser
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Role { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public DateTime RegisterationDate { get; set; } = DateTime.Now;
}