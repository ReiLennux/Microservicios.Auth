using Auth.Models.Dtos;

namespace API.Services.IServices
{
    public interface IAuthService
    {
        Task<string> Register(RegisterRequestDto requestDto);

        Task<LoginResponseDto> Login(LoginRequestDto responseDto);

        Task<bool> AssignRole(string email, string roleName);
    }
}
