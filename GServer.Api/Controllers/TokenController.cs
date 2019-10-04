using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using FluentValidation.AspNetCore;
using GServer.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GServer.Api.Controllers
{
    [Route("api/token")]
    [ApiController]
    [AllowAnonymous]
    public class TokenController: ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;

        public TokenController(
            UserManager<User> userManager,
            IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Post(
            [FromBody]
            [CustomizeValidator(Properties="Username,Password")]
            UserViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user_account = await userManager.FindByNameAsync(model.Username);
            if(user_account == null) {
                return Unauthorized();
            }

            var user = await userManager.CheckPasswordAsync(user_account, model.Password);
            if(user == false) {
                return Unauthorized();
            }

            var identityOptions = new IdentityOptions();
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(identityOptions.ClaimsIdentity.UserIdClaimType, user_account.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user_account.UserName),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };
            
            var access_token = CreateAccessToken(claims);

            return new OkObjectResult(
                new { token = access_token }
            );
        }

        public string CreateAccessToken(IEnumerable<Claim> claims) 
        {
            var token = new JwtSecurityToken
            (
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(5),
                notBefore: DateTime.UtcNow,
                audience: "Audience",
                issuer: "Issuer",
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JWT:Secret"])),
                        SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}