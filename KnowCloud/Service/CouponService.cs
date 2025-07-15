using KnowCloud.Models.Dto;
using KnowCloud.Service.Contract;
using static KnowCloud.Utility.Utilities;

namespace KnowCloud.Service
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;

        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto> GetAllCouponsAsync()
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = APIType.GET,
                Url = CouponAPIBase + "/api/coupon"
            });
        }

        public async Task<ResponseDto> GetCouponByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = APIType.GET,
                Url = CouponAPIBase + $"/api/coupon/{id}"
            });
        }

        public async Task<ResponseDto> GetCouponByCodeAsync(string code)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = APIType.GET,
                Url = CouponAPIBase + $"/api/coupon/GetByCode/{code}"
            });
        }

        public async Task<ResponseDto> CreateCouponAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = APIType.POST,
                Data = couponDto,
                Url = CouponAPIBase + "/api/coupon"
            });
        }

        public async Task<ResponseDto> UpdateCouponAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = APIType.PUT,
                Data = couponDto,
                Url = CouponAPIBase + "/api/coupon"
            });
        }

        public async Task<ResponseDto> DeleteCouponAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = APIType.DELETE,
                Url = CouponAPIBase + "/api/coupon?id=" + id
            });
        }
    }
}
