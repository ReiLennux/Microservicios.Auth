using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coupon.API.Models
{
    public class Coupon
    {
        [Key]
        public int CouponId { get; set; }
        [Required]
        public string CouponCode { get; set; }

        [Required]
        public double Discount { get; set; }

        public int MinAmount { get; set; }

        [NotMapped]
        public DateTime LastUpdated { get; set; }

        public string AmountType { get; set; }

        public int LimitUse { get; set; }

        public DateTime DateInit { get; set; }

        public DateTime DateEnd { get; set; }

        public string Category { get; set; }

        public bool StateCoupon { get; set; }

    }
}
