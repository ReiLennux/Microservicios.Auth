using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.API.Contract;
using ShoppingCart.API.Data;
using ShoppingCart.API.Models.Dtos;

namespace ShoppingCart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private ResponseDto _response;
        private IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly IProductService _productService;
        private readonly ICouponService _couponService;

        public ShoppingCartController (IMapper mapper, AppDbContext appDbContext, IProductService productService, ICouponService couponService)
        {
            _response = new ResponseDto();
            _mapper = mapper;
            _context = appDbContext;
            _productService = productService;
            _couponService = couponService;
        }

        //[HttpPost("ApplyCoupon")]
        //public async Task<object> ApplyCoupon([FromBody] CartDto cart)
        //{

        //}

        [HttpGet("GetCat/{userId}")]
        public async Task<ResponseDto> GetCart(string userId)
        {
            try
            {
                CartDto cart = new()
                {
                    CartHeader = _mapper.Map<CartHeaderDto>(_context.CartHeader.First(u => u.UserId == userId)),
                };
                cart.CartDetailsDtos = _mapper.Map<IEnumerable<CartDetailsDto>>(
                    _context.CartDetails.Where(u => u.CartHeaderId == cart.CartHeader.CartHeaderId));


                IEnumerable<ProductDto> productDtos = await _productService.getProducts();

                foreach (var item in cart.CartDetailsDtos)
                {
                    item.ProductDto = productDtos.FirstOrDefault(p => p.ProductId == item.ProductId);

                    cart.CartHeader.CartTotal
                }
            }
        }


    }
}
