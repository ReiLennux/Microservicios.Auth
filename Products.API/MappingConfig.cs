using AutoMapper;
using Products.API.Models;
using Products.API.Models.Dtos;

namespace Products.API
{
    public class MappingConfig
    {

        public static MapperConfiguration RegisterMaps() 
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>()
                .ReverseMap();
            });
            return mappingConfig;
        }
    }
}
