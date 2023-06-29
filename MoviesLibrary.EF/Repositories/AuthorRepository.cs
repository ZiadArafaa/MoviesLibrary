using MoviesLibrary.Core.Dtos;
using MoviesLibrary.Core.Repositories;
using MoviesLibrary.Core.Models;
using MoviesLibrary.Core.Settings;
using MoviesLibrary.EF.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MoviesLibrary.EF.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JWT _jwt;
        public AuthorRepository( UserManager<ApplicationUser> user, IOptions<JWT> options)   
        {
            _userManager = user;
            _jwt = options.Value;
        }
        public async Task<string?> RegisterAsync(ApplicationUser user, string password)
        {
            var Result = await _userManager.CreateAsync(user, password);

            return Result is not null ? string.Join(" , ", Result.Errors.Select(e => e.Description).ToList()) : null;
        }
        public async Task<AuthDto> LoginAync(string email, string password)
        {
            var User = await _userManager.FindByEmailAsync(email);
            if (User is null || !await _userManager.CheckPasswordAsync(User, password))
                return new AuthDto { Message = "Email Or Password incorrect" };

            User.Status = true;
            await _userManager.UpdateAsync(User);

            var jwt = await CreateTokenAsync(User);
            return new AuthDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwt),
                IsAuthenticated = true,
                ExpireOn = jwt.ValidTo,
                Email = User.Email,
                UserName = User.UserName,
            };
        }
        public async Task LogoutAsync(ApplicationUser user)
        {
            user.Status = false;
            await _userManager.UpdateAsync(user);
        }
        public async Task<ApplicationUser?> IsExistAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
        private async Task<JwtSecurityToken> CreateTokenAsync(ApplicationUser user)
        {
            var UserClaims = await _userManager.GetClaimsAsync(user);
            var Roles = await _userManager.GetRolesAsync(user);
            var RolesClaims = new List<Claim>();
            foreach (var Role in Roles)
            {
                RolesClaims.Add(new Claim("roles", Role));
            }

            var Claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim("UserId",user.Id)
            }.Union(RolesClaims).Union(UserClaims);

            var SymmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var SigningCredentials = new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                claims: Claims,
                audience: _jwt.Audience,
                issuer: _jwt.Issuer,
                expires: DateTime.Now.AddDays(_jwt.DurationOn),
                signingCredentials: SigningCredentials
                );
        }
    }
}
