using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Cookbook.Controllers
{
    [AllowAnonymous]
    [Area("MicrosoftIdentity")]
    [Route("[area]/[controller]/[action]")]
    public class MyAccountController : Controller
    {

        [HttpGet("{scheme?}")]
        public IActionResult SignIn([FromRoute] string scheme)
        {
            scheme ??= OpenIdConnectDefaults.AuthenticationScheme;
            var redirectUrl = Url.Content("~/");
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            properties.Items["policy"] = "B2C_1_susi";
            return Challenge(properties, scheme);
        }

        public IActionResult SignUp([FromRoute] string scheme)
        {
            scheme ??= OpenIdConnectDefaults.AuthenticationScheme;
            var redirectUrl = Url.Content("~/");
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            properties.Items["policy"] = "B2C_1_susi";
            return Challenge(properties, scheme);
        }

        [HttpGet("{scheme?}")]
        //public async Task<IActionResult> EditProfile([FromRoute] string scheme)
            public async Task<IActionResult> EditProfile([FromRoute] string scheme)
        {
            scheme ??= OpenIdConnectDefaults.AuthenticationScheme;
            var authenticated = await HttpContext.AuthenticateAsync(scheme).ConfigureAwait(false);
            if (!authenticated.Succeeded)
            {
                return Challenge(scheme);
            }

            var redirectUrl = Url.Content("~/microsoftidentity/account/editprofile");
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            //var properties = new AuthenticationProperties();
            properties.Items["policy"] = "B2C_1_edit_profile";
            return Challenge(properties, scheme);
        }

    }
}