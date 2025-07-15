

using KnowCloud.Models.Dto;

namespace KnowCloud.Service.Contract
{
    public interface IAuthService
    {
        Task<string> Register(RegisterRequestDto model);
        Task<LoginResponseDto> Login(LoginRequestDto model);
        Task<bool> AssignRole(string email, string role);

    }
}
