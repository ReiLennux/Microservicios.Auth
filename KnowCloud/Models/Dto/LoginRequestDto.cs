using System.ComponentModel.DataAnnotations;

namespace KnowCloud.Models.Dto
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage ="UserName is required.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
