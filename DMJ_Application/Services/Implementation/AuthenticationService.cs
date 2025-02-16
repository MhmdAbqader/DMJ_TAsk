using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DMJ_Application.Dtos;
using DMJ_Application.Services.Interfaces;
using DMJ_Application.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DMJ_Application.Services.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {


        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _access;
        private readonly JWT _jwt;

        public AuthenticationService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,
            IOptions<JWT> jwt, IHttpContextAccessor access)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
            _roleManager = roleManager;
            _access = access;
        }
        public async Task<AuthenticationResponseDto> Register(RegisterDto registerDto)
        {
            //var ExistUsername = await _userManager.FindByNameAsync(registerDto.Username);

            //if (ExistUsername != null)
            //    return new AuthenticationResponseDto { Message = "UserName Is Already Exists!" };
           
            var ExistEmail = await _userManager.FindByEmailAsync(registerDto.Email);
            if (ExistEmail != null)
                return new AuthenticationResponseDto { Message = "Email Is Already Exists!" };

            var user = new IdentityUser
            {                
                Email = registerDto.Email,
                UserName = registerDto.FirstName + "_" + registerDto.LastName,
                PhoneNumber = registerDto.PhoneNumber,
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                string err = string.Empty;
                foreach (var iterationError in result.Errors)
                {
                    err += iterationError.Description + "-";
                }
                return new AuthenticationResponseDto { Message = err };
            }

            if (!await _roleManager.RoleExistsAsync(SDRoles.AdminRole))
            {
                await _roleManager.CreateAsync(new IdentityRole() { Name = SDRoles.AdminRole });
                await _roleManager.CreateAsync(new IdentityRole(SDRoles.UserRole));
            }

            // based on user type but i added to admin to test all operation of authorization  
            await _userManager.AddToRoleAsync(user, SDRoles.AdminRole);
            var jwtSecurityToken = await CreateJWTToken(user);

            return new AuthenticationResponseDto
            {
                Email = user.Email,
                ExpireOn = jwtSecurityToken.ValidTo,
                Username = user.UserName,
                IsAuthenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Roles = new List<string>(await _userManager.GetRolesAsync(user)), //{ "Admin" }
            };
        }   

        public async Task<AuthenticationResponseDto> Login(LoginDto loginDto)
        {
            var authResponseDto = new AuthenticationResponseDto();
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            bool checkPassword = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (user == null || !checkPassword)
            {               
                return new AuthenticationResponseDto { Message = "Email or Password IS Invalid !" };
            }

            var jwtSecurityToken = await CreateJWTToken(user);
            var roleList = await _userManager.GetRolesAsync(user);


            authResponseDto.IsAuthenticated = true;
            authResponseDto.Email = user.Email;
            authResponseDto.Username = user.UserName;
            authResponseDto.ExpireOn = jwtSecurityToken.ValidTo;
            authResponseDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authResponseDto.Roles = roleList.ToList();                    

            return authResponseDto;
        }

        private async Task<JwtSecurityToken> CreateJWTToken(IdentityUser user)
        {

            var userclaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleclaims = new List<Claim>();
            foreach (var role in roles)
            {
                //roleclaims.Add(new Claim("roles",role));
                roleclaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var cliams = new[] {
                 new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                 new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                 new Claim(JwtRegisteredClaimNames.Email,user.Email),
                 new Claim(ClaimTypes.NameIdentifier,user.Id)
                 //new Claim("uid",user.Id)
            }.Union(userclaims).Union(roleclaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.key));
            var signingcredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.issuer,
                audience: _jwt.audience,
                claims: cliams,
                expires: DateTime.Now.AddMinutes(_jwt.expireIn), // i added minutes to test expiration
                signingCredentials: signingcredentials
                );

            return jwtSecurityToken;
        }

    }
}
