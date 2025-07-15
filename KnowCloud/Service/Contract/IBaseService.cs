

using KnowCloud.Models.Dto;

namespace KnowCloud.Service.Contract
{
    public interface IBaseService
    {
        Task<ResponseDto> SendAsync<T>(RequestDto requestDto, bool withBearerToken = true);
        Task<ResponseDto> SendAsync(RequestDto requestDto, bool withBearerToken = true);
    }

}

