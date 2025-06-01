using API.Data;
using API.Services.IServices;
using Auth.Models;
using Auth.Models.Dtos;
using Microsoft.AspNetCore.Identity;

namespace API.Services
{
    public class AuthService: IAuthService
    {

        private readonly AppDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _context.ApplicationUser
                .FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());
            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (user == null && isValid == false) 
            {
                return new LoginResponseDto() { User = null, Token = ""};
            }

            var roles = await _userManager.GetRolesAsync(user);

            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            UserDto userDto = new()
            {
                Email = user.Email,
                Id = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
            };

            LoginResponseDto loginResponseDto = new()
            {
                Token = token,
                User = userDto
            };
            return loginResponseDto;
        }


        public async Task<UserDto> RegisterUser(RegisterRequestDto requestDto)
        {
            ApplicationUser user = new()
            {
                UserName = requestDto.Email,
                Email = requestDto.Email,
                NormalizedEmail = requestDto.Email.ToUpper(),
                Name = requestDto.Name,
                PhoneNumber = requestDto.PhoneNumber,
            };

            try
            {
                var result = await _userManager.CreateAsync(user, requestDto.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _context.ApplicationUser
                        .First(u => u.UserName == requestDto.Email);
                    UserDto userDto = new()
                    {
                        Email = userToReturn.Email,
                        Id = userToReturn.Id,
                        Name = userToReturn.Name,
                    };
                    return userDto;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return new UserDto();
        }


        public async Task<String> Register(RegisterRequestDto requestDto)
        {
            ApplicationUser user = new()
            {
                UserName = requestDto.Email,
                Email = requestDto.Email,
                NormalizedEmail = requestDto.Email.ToUpper(),
                Name = requestDto.Name,
                PhoneNumber = requestDto.PhoneNumber,
            };

            try
            {
                var result = await _userManager.CreateAsync(user, requestDto.Password);

                if (result.Succeeded)
                {
                    var userToReturn = _context.ApplicationUser
                        .First(u => u.UserName == requestDto.Email);
                    UserDto userDto = new()
                    {
                        Email = userToReturn.Email,
                        Id = userToReturn.Id,
                        Name = userToReturn.Name,
                        PhoneNumber = userToReturn.PhoneNumber
                    };
                    return "";
                } 
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message.ToLower();
            }
            return "Error Encountered";
        }


        public async Task<bool> AssignRole (string email, string roleName)
        {
            var user = _context.ApplicationUser
                .FirstOrDefault(u => u.Email.ToLower() == email.ToLower());

            if (user != null)
            {
                if(!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }
    }
}
