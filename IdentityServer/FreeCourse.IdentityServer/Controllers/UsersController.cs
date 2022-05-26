using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using FreeCourse.IdentityServer.Dtos;
using FreeCourse.IdentityServer.Models;
using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static IdentityServer4.IdentityServerConstants;

namespace FreeCourse.IdentityServer.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDto signUpDto)
        {
            var applicationUser = new ApplicationUser()
            {
                UserName = signUpDto.UserName,
                Email = signUpDto.Email,
                City = signUpDto.City
            };
            var result = await _userManager.CreateAsync(applicationUser, signUpDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(Response<NoContent>.Fail(result.Errors.Select(x => x.Description).ToList(), 400));
            }
            return NoContent() ;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            //userIdClaim token içinde bulunan sub claiminin içindeki verileri tutar. Value'su da burada user id bilgisi
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null ) { return BadRequest(); }
            var user = await _userManager.FindByIdAsync(userIdClaim.Value);
            if (user == null) { return BadRequest(); }
            return Ok(new { Id = user.Id, UserName = user.UserName, Email = user.Email, City = user.City });
        }
    }
}
