using AuthAPI.Core.Dtos;
using CoreLib.Interfaces;

namespace AuthAPI.Core.Interfaces;

public interface IUser
{
    Task<ResponseMessage> RegisterAsync(AppUserDto appUserDto);
    Task<ResponseMessage> LoginAsync(LoginDto loginDto);
    Task<GetUserDto> GetUserAsync(int userId);
}