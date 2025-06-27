using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowCloud.Models.Dto
{
    public class RegisterRequestDto
    {
        [Required]
        public string Email { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]

        public string Password { get; set; }
        [Required]

        public string PhoneNumber { get; set; }
        public string Role { get; set; }
    }
}
