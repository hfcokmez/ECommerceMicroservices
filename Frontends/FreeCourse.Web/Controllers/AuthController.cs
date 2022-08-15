using System;
using System.Threading.Tasks;
using FreeCourse.Web.Models.Inputs;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IIdentityService _identityService;

        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public IActionResult SingIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SingIn(SignInInput signInInput)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var response = await _identityService.SignIn(signInInput);
            if (!response.IsSuccessful)
            {
                response.Errors.ForEach(error =>
                {
                    ModelState.AddModelError(String.Empty, error);
                });
                return View();
            } 
            return RedirectToAction(nameof(Index), "Home");
        }
    }
}