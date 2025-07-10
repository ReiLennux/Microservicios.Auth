
using KnowCloud.Models.Dto;

namespace KnowCloud.Service.Contract
{
    public interface ICartService
    {
        Task<ResponseDto> GetCartByUserIdAsync(string userId);
        Task<ResponseDto> UpsertCartAsync(CartDto cartDto);
        Task<ResponseDto> RemoveCartAsync(int cartDetailsId);
        Task<ResponseDto> ApplyCouponAsync(CartDto cartDto);
        //Task<ResponseDto> RemoveCouponAsync(string userId);
        Task<ResponseDto> EmailCart(CartDto CartDto);
    }
}