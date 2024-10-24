using LinkDev.Talabat.Core.Application.Abstraction.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Auth.Models;
using LinkDev.Talabat.Core.Application.Exceptions;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LinkDev.Talabat.Core.Application.Services.Auth
{
    public class AuthService(
        IOptions<JWTSettings> jwtSettings,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager) : IAuthService
    {

        private readonly JWTSettings _jwtSettings = jwtSettings.Value;

        public async Task<UserDto> LoginAsync(LoginDto model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user is null)
                throw new UnAuthorizedException("Invalid Login.");

            var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: true);


            if (result.IsNotAllowed) throw new UnAuthorizedException("Account not confirmed yet.");

            if (result.IsLockedOut) throw new UnAuthorizedException("Account is Locked.");

            //if (result.RequiresTwoFactor) throw new UnAuthorizedException("Requires Two-Factor Authentication.");

            if (!result.Succeeded) throw new UnAuthorizedException("Invalid Login.");


            var response = new UserDto()
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = user.Email!,
                Token = await GenerateTokenAsync(user)
            };

            return response;

        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new ApplicationUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.DisplayName,
                PhoneNumber = registerDto.Phone
            };

            var result = await userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
                throw new ValidationException()
                {
                    Errors = result.Errors.Select(E => E.Description)
                };

            var response = new UserDto()
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = user.Email!,
                Token = await GenerateTokenAsync(user)
            };

            return response;
        }

        private async Task<string> GenerateTokenAsync(ApplicationUser user)
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            var rolesAsClaims = new List<Claim>();

            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
                rolesAsClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.PrimarySid,user.Id),
                new Claim(ClaimTypes.Email,user.Email!),
                new Claim(ClaimTypes.GivenName,user.DisplayName),
            }
            .Union(userClaims)
            .Union(rolesAsClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));


            var tokenObj = new JwtSecurityToken(

                audience: _jwtSettings.Audience,
                issuer: _jwtSettings.Issuer,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMintues),
                claims: claims,
                signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenObj);
        }

    }
}
