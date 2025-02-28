using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthAPI.Core.Dtos;
using AuthAPI.Core.Interfaces;
using AuthAPI.Core.Models;
using AuthAPI.Infrastructure.Data;
using CoreLib.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AuthAPI.Infrastructure.Repos;

public class UserRepo : IUser
{
    private readonly AuthDbContext _context;
    private readonly IConfiguration _config;

    public UserRepo(AuthDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public async Task<GetUserDto> GetUserAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);

        if (user is null)
            return null!;

        return new GetUserDto(user.Id, user.Name!, user.Role!, user.Email!);
    }

    public async Task<ResponseMessage> LoginAsync(LoginDto loginDto)
    {
        var user = await GetUserByEmail(loginDto.Email!);
        if (user is null)
            return new ResponseMessage(false, "Invalid Credentials");

        bool verifiedPassword = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password);

        if (!verifiedPassword)
            return new ResponseMessage(false, "Invalid Credentials");

        string token = GenerateToken(user);
        return new ResponseMessage(true, token);
    }

    public async Task<ResponseMessage> RegisterAsync(AppUserDto appUserDto)
    {
        var user = await GetUserByEmail(appUserDto.Email!);
        if (user is not null)
            return new ResponseMessage(false, "Email Is Used");

        var result = _context.Users.Add(new AppUser()
        {
            Name = appUserDto.Name,
            Email = appUserDto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(appUserDto.Password),
            Role = appUserDto.Role
        });

        await _context.SaveChangesAsync();

        if (result.Entity.Id > 0)
            return new ResponseMessage(true, "Registered Successfully!");
        return new ResponseMessage(false, "Invalid Data");
    }

    private async Task<AppUser> GetUserByEmail(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

        if (user is null)
            return null!;
        return user;
    }

    private string GenerateToken(AppUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Issuer = _config["Jwt:Issuer"],
            Audience = _config["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SigningKey"]!)), SecurityAlgorithms.HmacSha256),
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Name!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Role, user.Role!)
            })
        };

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(securityToken);
    }
}
