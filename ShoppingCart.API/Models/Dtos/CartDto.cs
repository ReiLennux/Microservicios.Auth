﻿namespace ShoppingCart.API.Models.Dtos
{
    public class CartDto
    {
        public CartHeaderDto CartHeader { get; set; }

        public IEnumerable<CartDetailsDto> CartDetailsDtos { get; set; }
    }
}
