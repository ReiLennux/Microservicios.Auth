using KnowCloud.Models.Dto;
using KnowCloud.Service.Contract;

namespace KnowCloud.Service
{
    public class ProductService : IProductService
    {
        private readonly IBaseService _baseService;

        public ProductService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto> CreateProductAsync(ProductDto productDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.Utilities.APIType.POST,
                Data = productDto,
                Url = Utility.Utilities.ProductAPIBase + "/api/product",
                ContentType = Utility.Utilities.ContentType.MultipartFormData
            });
        }

        public async Task<ResponseDto> DeleteProductAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.Utilities.APIType.DELETE,
                Url = Utility.Utilities.ProductAPIBase + "/api/Product/" + id
            });
        }

        public async Task<ResponseDto> GetAllProductsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.Utilities.APIType.GET,
                Url = Utility.Utilities.ProductAPIBase + "/api/Product"
            }, false);
        }

        public async Task<ResponseDto> GetProductByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.Utilities.APIType.GET,
                Url = Utility.Utilities.ProductAPIBase + "/api/Product/" + id
            });
        }

        public async Task<ResponseDto> UpdateProductAsync(ProductDto productDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.Utilities.APIType.PUT,
                Data = productDto,
                Url = Utility.Utilities.ProductAPIBase + "/api/Product",
                ContentType = Utility.Utilities.ContentType.MultipartFormData
            });
        }
    }
}
