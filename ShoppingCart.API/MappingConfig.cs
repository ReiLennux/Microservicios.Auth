using AutoMapper;
using ShoppingCart.API.Models;
using ShoppingCart.API.Models.Dtos;

namespace ShoppingCart.API
{
    public class MappingConfig
    {

        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CartDetailsDto, CartDetails>()
                .ReverseMap();
                config.CreateMap<CartHeaderDto, CartHeaderDto>()
                .ReverseMap();
            });
            return mappingConfig;
        }
    }
}
