

namespace KnowCloud.Service.Contract
{
    public interface IBaseService
    {
        Task<ResponseDto> SendAsync<T>(RequestDto requestDto, bool withBearerToken = true);
    }

}

