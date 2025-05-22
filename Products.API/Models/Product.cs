using System.ComponentModel.DataAnnotations;

namespace Products.API.Models
{
    public class Product
    {
        [Key]
        public int ProductID {  get; set; }
        [Required]
        public string Name { get; set; }
        [Range(1,100)]
        public double Price { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
        public string ImageLocalPath {  get; set; }
    }
}
