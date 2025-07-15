using KnowCloud.Models.Dto;
using KnowCloud.Service.Contract;
using KnowCloud.Utility;

namespace KnowCloud.Service
{
    public class CartService : ICartService
    {
        private readonly IBaseService _baseService;

        public CartService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto> ApplyCouponAsync(CartDto cartDto)
        {
            return await _baseService.SendAsync<ResponseDto>(new RequestDto()
            {
                ApiType = Utilities.APIType.POST,
                Data = cartDto,
                Url = Utilities.ShoppingCartAPIBase + "/api/CartApi/applyCoupon"
            });
        }

        public async Task<ResponseDto> EmailCart(CartDto CartDto)
        {
            return await _baseService.SendAsync<ResponseDto>(new RequestDto()
            {
                ApiType = Utilities.APIType.POST,
                Data = CartDto,
                Url = Utilities.ShoppingCartAPIBase + ""
            });
        }

        public async Task<ResponseDto> GetCartByUserIdAsync(string userId)
        {
            return await _baseService.SendAsync<ResponseDto>(new RequestDto()
            {
                ApiType = Utilities.APIType.GET,
                Url = Utilities.ShoppingCartAPIBase + "/api/CartApi/GetCart/" + userId
            });
        }

        public async Task<ResponseDto> RemoveCartAsync(int cartDetailsId)
        {
            return await _baseService.SendAsync<ResponseDto>(new RequestDto()
            {
                ApiType = Utilities.APIType.DELETE,
                Data = cartDetailsId,
                Url = Utilities.ShoppingCartAPIBase + "/api/CartApi/RemoveCart"
            });
        }

        public async Task<ResponseDto> UpsertCartAsync(CartDto cartDto)
        {
            return await _baseService.SendAsync<ResponseDto>(new RequestDto()
            {
                ApiType = Utilities.APIType.POST,
                Url = Utilities.ShoppingCartAPIBase + "/api/CartApi/UpsertCart",
                Data = cartDto,
            });
        }
    }
}
