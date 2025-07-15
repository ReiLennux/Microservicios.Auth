using KnowCloud.Models.Dto;

namespace KnowCloud.Service.Contract
{
        public interface ICouponService
        {
            Task<ResponseDto> GetAllCouponsAsync();
            Task<ResponseDto> GetCouponByIdAsync(int id);
            Task<ResponseDto> GetCouponByCodeAsync(string code);
            Task<ResponseDto> CreateCouponAsync(CouponDto couponDto);
            Task<ResponseDto> UpdateCouponAsync(CouponDto couponDto);
            Task<ResponseDto> DeleteCouponAsync(int id);
        }

}
