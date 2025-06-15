using ShoppingCart.API.Models.Dtos;

namespace ShoppingCart.API.Contract
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> getProducts();
    }
}
