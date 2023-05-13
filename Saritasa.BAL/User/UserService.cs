using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ViewModels.User;

namespace Saritasa.BAL.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<Data.Entities.User> _userManager;
        private readonly SignInManager<Data.Entities.User> _signInManager;
        // Identity libaries
        public UserService(UserManager<Data.Entities.User> userManager, SignInManager<Data.Entities.User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<string> CreateAsync(RegisterUserRequest request)
        {
            // Check is this user exist on system --> If yes, deny
            var checkUser = await _userManager.FindByEmailAsync(request.Email);
            if (checkUser != null) return "This email is exist in system";
            var user = new Data.Entities.User()
            {
                UserName = request.Email,
                Email = request.Email

            };
            // Create user
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return "Account was created successfully";
            }
            return "Error";
        }

        public async Task<string> Login(LoginUserRequest request)
        {
            // Check email is exist in system
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) return "Wrong email";
            // Check password is it correct
            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);
            if (!result.Succeeded) return "Wrong password";

            // Valid information - Generate token
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };
            // Key: 0123456789ASDQWE; Issuer: Saritasa.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("0123456789ASDQWE"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Token will be expired before 3 hours
            var token = new JwtSecurityToken("Saritasa",
                "Saritasa",
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
