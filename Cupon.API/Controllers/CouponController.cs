using AutoMapper;
using Coupon.API.Data;
using Coupon.API.Models;
using Coupon.API.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coupon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private ResponseDto _response;
        private IMapper _mapper;

        public CouponController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Models.Coupon> couponList = _dbContext.Coupons.ToList();
                _response.Result = _mapper.Map<IEnumerable<CouponDto>>(couponList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("{id}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Models.Coupon coupon = _dbContext.Coupons.FirstOrDefault(c => c.CouponId == id);
                if (coupon == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Coupon not found";
                }
                else
                {
                    _response.Result = _mapper.Map<CouponDto>(coupon);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("GetByCode/{code}")]
        public ResponseDto GetByCode(string code)
        {
            try
            {
                Models.Coupon coupon = _dbContext.Coupons.FirstOrDefault(c => c.CouponCode.ToLower() == code.ToLower());
                if (coupon == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Coupon not found";
                }
                else
                {
                    _response.Result = _mapper.Map<CouponDto>(coupon);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        [Authorize(Roles = "ADMINISTRADOR, VENTAS")]
        public ResponseDto Post([FromBody] CouponDto couponDto)
        {
            try
            {
                Models.Coupon coupon = _mapper.Map<Models.Coupon>(couponDto);
                _dbContext.Coupons.Add(coupon);
                _dbContext.SaveChanges();

                //Agregar stripe

                _response.Result = _mapper.Map<CouponDto>(coupon);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        //[HttpPut]
        //[Authorize(Roles = "ADMINISTRADOR")]
        //public ResponseDto Put([FromBody] CouponDto couponDto)
        //{
        //    try
        //    {
        //        Models.Coupon coupon = _dbContext.Coupons.FirstOrDefault(c => c.CouponId == couponDto.CouponId);
        //        if (coupon == null)
        //        {
        //            _response.IsSuccess = false;
        //            _response.Message = "Coupon not found";
        //        }
        //        else
        //        {
        //            coupon.CouponCode = couponDto.CouponCode;
        //            coupon.Discount = couponDto.Discount;
        //            coupon.MinAmount = couponDto.MinAmount;
        //            coupon.LastUpdated = DateTime.Now;
        //            coupon.AmountType = couponDto.AmountType;
        //            coupon.LimitUse = couponDto.LimitUse;
        //            coupon.DateInit = couponDto.DateInit;
        //            coupon.DateEnd = couponDto.DateEnd;
        //            coupon.Category = couponDto.Category;
        //            coupon.StateCoupon = couponDto.StateCoupon;
        //            _dbContext.Coupons.Update(coupon);
        //            _dbContext.SaveChanges();
        //            _response.Result = _mapper.Map<CouponDto>(coupon);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.Message = ex.Message;
        //    }
        //    return _response;
        //}

        [HttpPut]
        [Authorize(Roles = "ADMINISTRADOR")]
        public ResponseDto Put([FromBody] CouponDto request)
        {
            try
            {

                Models.Coupon obj = _mapper.Map<Models.Coupon>(request);
                _dbContext.Coupons.Update(obj);
                _dbContext.SaveChanges();

                _response.Result = _mapper.Map<CouponDto>(obj);

            } catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        [Authorize(Roles = "ADMINISTRADOR")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Models.Coupon coupon = _dbContext.Coupons.FirstOrDefault(c => c.CouponId == id);
                if (coupon == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Coupon not found";
                }
                else
                {
                    _dbContext.Coupons.Remove(coupon);
                    _dbContext.SaveChanges();
                    _response.Result = "Coupon deleted successfully";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
