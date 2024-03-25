using ClassSystem.Core.Helpers;
using ClassSystem.Core.Interfaces;
using ClassSystem.Core.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ClassSystem.EF.Repositories
{
    public class StudentsRepository : BaseRepository<Student>, IStudentsRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;

        public StudentsRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IOptions<JWT> jwt, RoleManager<IdentityRole> roleManager) : base(context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
            
        }

        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) != null)
            {
                return new AuthModel { Massage = "Email is already registered!" };
            }
            if (await _userManager.FindByNameAsync(model.UserName) != null)
            {
                return new AuthModel { Massage = "Username is already registered!" };
            }
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {

                    errors += $"{error.Description} ,";
                }
                return new AuthModel { Massage = errors };
            }
            await _userManager.AddToRoleAsync(user, "User");
            var jwtSecurityToken = await CreateJwtToken(user);
            return new AuthModel
            {
                Email = user.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                Username = user.UserName,
                IsAuthenticated = true,
                Roles = new List<string> { "User"},
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            };
        }

        public async Task<AuthModel> GetJwtToken(TokenRequestModel model)
        {
            var authModel = new AuthModel();
            var user = await _userManager.FindByNameAsync(model.Username);
            var pass = await _userManager.CheckPasswordAsync(user, model.Password);
            if (user is null || !pass)
            {
                authModel.Massage = "Username Or Passwsord is incorrect CHECK YOU CREDENTIALS!";
                return authModel;
            }
            var jwtSecurityToken = await CreateJwtToken(user);
            var rolesList = await _userManager.GetRolesAsync(user);
            authModel.Username = user.UserName;
            authModel.Roles = rolesList.ToList();
            authModel.IsAuthenticated = true;
            authModel.Email = user.Email;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            return authModel;
        }
        public async Task<string> AddToRoleAsync(AddToRoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            var role = await _roleManager.RoleExistsAsync(model.Role);
            if(user is null || !await _roleManager.RoleExistsAsync(model.Role))
            {
                return "UserId or role is not valid!";
            }
            if(await _userManager.IsInRoleAsync(user , model.Role))
            { 
                return "User is already assigned to this role!";
            }
            var result = await _userManager.AddToRoleAsync(user, model.Role);
            //if (!result.Succeeded)
            //{
            //    return "Somthing went wrong please try again !";
            //}
            //return string.Empty;
            return result.Succeeded ? string.Empty : "Somthing went wrong please try again !";
        }
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        public async Task<Student> SpecialMethod()
        {
            throw new NotImplementedException();
        }
    }
}
