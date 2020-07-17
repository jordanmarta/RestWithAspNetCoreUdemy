using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithAspNetCoreUdemy.Models;
using RestWithAspNetCoreUdemy.Services.Interfaces;

namespace RestWithAspNetCoreUdemy.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LoginController : Controller
    {
        private ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody]UserVO user)
        {
            if (user == null) return BadRequest();
            return Ok(_loginService.FindByLogin(user));
        }
    }
}
