using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.API.Contract;
using ShoppingCart.API.Data;
using ShoppingCart.API.Models;
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


        [HttpPost("ApplyCoupon")]
        public async Task<object> ApplyCoupon([FromBody] CartDto cartDto)
        {
            try
            {
                var cartFromDb = await _context.CartHeader.FirstAsync(u => u.UserId == cartDto.CartHeader.UserId);
                cartFromDb.CouponCode = cartDto.CartHeader.CouponCode;
                _context.CartHeader.Update(cartFromDb);
                await _context.SaveChangesAsync();
                _response.Result = true;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPost("RemoveCoupon")]
        public async Task<object> RemoveCoupon([FromBody] CartDto cartDto)
        {
            try
            {
                var cartFromDb = await _context.CartHeader.FirstAsync(u => u.UserId == cartDto.CartHeader.UserId);
                cartFromDb.CouponCode = string.Empty;
                _context.CartHeader.Update(cartFromDb);
                await _context.SaveChangesAsync();
                _response.Result = true;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpGet("GetCart/{userId}")]
        public async Task<ResponseDto> GetCart(string userId)
        {
            try
            {
                CartDto cart = new()
                {
                    CartHeader = _mapper.Map<CartHeaderDto>(_context.CartHeader.FirstOrDefault(u => u.UserId == userId))
                };

                cart.CartDetailsDtos = _mapper.Map<IEnumerable<CartDetailsDto>>(
                    _context.CartDetails.Where(u => u.CartHeaderId == cart.CartHeader.CartHeaderId));

                IEnumerable<ProductDto> productList = await _productService.getProducts();

                foreach (var item in cart.CartDetailsDtos)
                {
                    item.ProductDto = productList.FirstOrDefault(u => u.ProductId == item.ProductId);
                    cart.CartHeader.CartTotal += (item.Count * item.ProductDto.Price);
                }

                if (!string.IsNullOrEmpty(cart.CartHeader.CouponCode))
                {
                    CouponDto coupon = await _couponService.GetCoupon(cart.CartHeader.CouponCode);
                    if (coupon != null && cart.CartHeader.CartTotal > coupon.MinAmount)
                    {
                        cart.CartHeader.CartTotal -= coupon.DiscountAmount;
                        cart.CartHeader.Discount = coupon.DiscountAmount;
                    }
                }
                _response.Result = cart;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPost("UpsertCart")]
        public async Task<ResponseDto> UpsertCart([FromBody] CartDto cartDto)
        {
            try
            {
                var cartHeaderFromDb = await _context.CartHeader.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == cartDto.CartHeader.UserId);

                if (cartHeaderFromDb == null)
                {
                    CartHeader cartHeader = _mapper.Map<CartHeader>(cartDto.CartHeader);
                    _context.CartHeader.Add(cartHeader);
                    await _context.SaveChangesAsync();
                    cartDto.CartDetailsDtos.First().CartHeaderId = cartHeader.CartHeaderId;
                    _context.CartDetails.Add(_mapper.Map<CartDetails>(cartDto.CartDetailsDtos.First()));
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var cartDetailsFromDb = await _context.CartDetails.AsNoTracking()
                        .FirstOrDefaultAsync(u => u.ProductId == cartDto.CartDetailsDtos.First().ProductId
                        && u.CartHeaderId == cartHeaderFromDb.CartHeaderId);

                    if (cartDetailsFromDb == null)
                    {
                        cartDto.CartDetailsDtos.First().CartHeaderId = cartHeaderFromDb.CartHeaderId;
                        _context.CartDetails.Add(_mapper.Map<CartDetails>(cartDto.CartDetailsDtos.First()));
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        cartDto.CartDetailsDtos.First().Count += cartDetailsFromDb.Count;
                        cartDto.CartDetailsDtos.First().CartHeaderId = cartDetailsFromDb.CartHeaderId;
                        cartDto.CartDetailsDtos.First().CartDetailsId = cartDetailsFromDb.CartDetailsId;
                        _context.CartDetails.Update(_mapper.Map<CartDetails>(cartDto.CartDetailsDtos.First()));
                        await _context.SaveChangesAsync();
                    }
                }
                _response.Result = cartDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost("RemoveCart")]
        public async Task<ResponseDto> RemoveCart([FromBody] int cartDetailsId)
        {
            try
            {
                CartDetails cartDetails = _context.CartDetails.First(u => u.CartDetailsId == cartDetailsId);

                int totalCountOfCartItem = _context.CartDetails.Where(u => u.CartHeaderId == cartDetails.CartHeaderId).Count();
                _context.CartDetails.Remove(cartDetails);
                if (totalCountOfCartItem == 1)
                {
                    var cartHeaderToRemove = await _context.CartHeader
                        .FirstOrDefaultAsync(u => u.CartHeaderId == cartDetails.CartHeaderId);
                    _context.CartHeader.Remove(cartHeaderToRemove);
                }
                await _context.SaveChangesAsync();
                _response.Result = true;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;
            }
            return _response;
        }
    }

}

