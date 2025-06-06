using AutoMapper;
using Coupon.API.Models.Dtos;

namespace Coupon.API
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CouponDto, Models.Coupon>()
                .ReverseMap();
            });
            return mappingConfig;
        }
    }
}
