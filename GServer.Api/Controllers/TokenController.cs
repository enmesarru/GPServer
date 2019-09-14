using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using AutoMapper;
using FluentValidation.AspNetCore;
using GServer.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        public TokenController(UserManager<User> userManager, IConfiguration configuration)
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
            var user = await userManager.CheckPasswordAsync(user_account, model.Password);

            if(user_account == null) {
                return Unauthorized();
            }
            
            if(user == false) {
                return Unauthorized();
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, model.Username),
            };
            Console.WriteLine(configuration["JWT:Secret"]);
            var token = new JwtSecurityToken
            (
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                notBefore: DateTime.UtcNow,
                audience: "Audience",
                issuer: "Issuer",
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JWT:Secret"])),
                        SecurityAlgorithms.HmacSha256)
            );

            return new OkObjectResult(
                new { token =  new JwtSecurityTokenHandler().WriteToken(token) }
            );
        }
    }
}