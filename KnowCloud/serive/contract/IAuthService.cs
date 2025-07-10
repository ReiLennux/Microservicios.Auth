
using KnowCloud.Dto;

public interface IAuthService
{
    Task<ResponseDto> LoginAsync(LoginDto loginDto);
    Task<ResponseDto> RegisterAsync(RegisterDto registerDto);
    Task<ResponseDto> AssignRoleAsync(RegisterDto registerDto);

}