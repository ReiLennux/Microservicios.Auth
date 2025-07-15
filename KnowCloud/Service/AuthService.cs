using KnowCloud.Models.Dto;
using KnowCloud.Service.Contract;
using static KnowCloud.Utility.Utilities;
using Newtonsoft.Json;

namespace KnowCloud.Service
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;

        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<string> Register(RegisterRequestDto model)
        {
            var response = await _baseService.SendAsync(new RequestDto
            {
                ApiType = APIType.POST,
                Data = model,
                Url = AuthAPIBase + "/api/AuthApi/register"
            });

            if (response != null && !response.IsSuccess)
                return response.Message;

            return string.Empty; // éxito
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto model)
        {
            var response = await _baseService.SendAsync(new RequestDto
            {
                ApiType = APIType.POST,
                Data = model,
                Url = AuthAPIBase + "/api/AuthApi/login"
            });

            if (response != null && response.IsSuccess)
            {
                return JsonConvert.DeserializeObject<LoginResponseDto>(response.Result.ToString());
            }

            return new LoginResponseDto(); // retorna objeto vacío si falla
        }

        public async Task<bool> AssignRole(string email, string role)
        {
            var requestDto = new RegistrationRequstDto
            {
                Email = email,
                Role = role
            };

            var response = await _baseService.SendAsync(new RequestDto
            {
                ApiType = APIType.POST,
                Data = requestDto,
                Url = AuthAPIBase + "/api/AuthApi/assignRole"
            });

            return response != null && response.IsSuccess;
        }
    }
}
