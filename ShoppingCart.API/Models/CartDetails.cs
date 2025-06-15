using ShoppingCart.API.Models.Dtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.API.Models
{
    public class CartDetails
    {
        [Key]
        public int CartDetailsId { get; set; }

        [ForeignKey("CartHeaderId")]
        public int CartHeaderId { get; set; }

        public CartHeader CartHeader { get; set; }
        public int ProductId { get; set; }

        [NotMapped]
        public ProductDto ProductDto { get; set; }

        public int Count { get; set; }
    }
}
