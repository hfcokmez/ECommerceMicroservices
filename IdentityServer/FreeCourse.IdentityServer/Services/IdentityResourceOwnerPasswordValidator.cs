using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FreeCourse.IdentityServer.Models;
using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;

namespace FreeCourse.IdentityServer.Services
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
         
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var existedUser = await _userManager.FindByEmailAsync(context.UserName);
            if (existedUser == null)
            {
                var errors = new Dictionary<string, object>();
                errors.Add("errors", new List<string> { "Email veya şifreniz yanlış" });
                context.Result.CustomResponse = errors;
                return;
            }

            bool passwordCheck = await _userManager.CheckPasswordAsync(existedUser, context.Password);
            if (!passwordCheck)
            {
                var errors = new Dictionary<string, object>();
                errors.Add("errors", new List<string> { "Email veya şifreniz yanlış" });
                context.Result.CustomResponse = errors;
                return;
            }

            context.Result = new GrantValidationResult(existedUser.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
        }
    }
}

