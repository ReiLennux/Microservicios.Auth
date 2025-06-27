using System.ComponentModel.DataAnnotations;

namespace KnowCloud.Models
{
    public class RequestWrapper<T>
    {
        [Required]
        public T Body { get; set; }
    }
}
