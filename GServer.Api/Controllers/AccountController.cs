using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using GServer.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GServer.Api.Controllers
{
    [Authorize]
    [Route("api/account")]
    [ApiController]
    public class AccountController: ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly IEmailSender emailSender;

        private readonly IMapper mapper;

        public AccountController(
            UserManager<User> userManager,
            IEmailSender emailSender,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.emailSender = emailSender;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = mapper.Map<User>(model);
            IdentityResult result = await userManager.CreateAsync(user, model.Password);

            if(!result.Succeeded) {
                return BadRequest(result);
            }
            return new OkObjectResult(StatusCodes.Status201Created);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> Put(
            string userId,
            [FromBody]
            UserResetPasswordViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var user = await userManager.FindByIdAsync(userId);
            if(user is null) {
                return NotFound();
            }
            var checkUserPassword = await userManager
                .CheckPasswordAsync(user, model.OldPassword);
            
            if(checkUserPassword)
            {
                var result =  await userManager
                    .ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                
                if(!result.Succeeded)
                    return BadRequest(result.Errors);
            
                return Ok(StatusCodes.Status200OK);
            } 
            else
            {
                return NotFound();
            }
        }
    }
}