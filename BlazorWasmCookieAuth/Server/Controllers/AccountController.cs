using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWasmCookieAuth.Server.Controllers
{
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        [HttpGet("Login")]
        public ActionResult Login(string returnUrl = "/")
        {
            if (!Url.IsLocalUrl(returnUrl))
            {
                ModelState.AddModelError(nameof(returnUrl), "Value must be a local URL");
                return BadRequest(ModelState);
            }

            return Challenge(new AuthenticationProperties { RedirectUri = returnUrl });
        }

        [HttpGet("Logout")]
        public IActionResult Logout() => SignOut(
            new AuthenticationProperties { RedirectUri = "/" },
            CookieAuthenticationDefaults.AuthenticationScheme,
            OpenIdConnectDefaults.AuthenticationScheme);
    }
}
