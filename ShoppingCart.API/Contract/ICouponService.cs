using ShoppingCart.API.Models.Dtos;

namespace ShoppingCart.API.Contract
{
    public interface ICouponService
    {
        Task<CouponDto> GetCoupon(string code);
    }
}
