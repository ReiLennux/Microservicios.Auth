using Newtonsoft.Json;
using ShoppingCart.API.Contract;
using ShoppingCart.API.Models.Dtos;

namespace ShoppingCart.API.Services
{
    public class CouponService : ICouponService
    {
        private readonly IHttpClientFactory _clientFactory;

        public CouponService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<CouponDto> GetCoupon (string code)
        {
            var client = _clientFactory.CreateClient("Coupon");

            var response = await client.GetAsync($"/api/Coupon/GetByCode/{code}");

            var apiContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseDto>(apiContent);

            if(result != null && result.IsSuccess)
            {
                return JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(result.Result));
            }
            return new CouponDto();
        }
    }
}
