using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiJwt.Models;

namespace WebApiJwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        [HttpGet("[action]")]
        public IActionResult Test()
        {
            return Ok(new CreateToken().TokenCreate());
        }
        [HttpGet("[action]")]
        public IActionResult AdminTest()
        {
            return Ok(new CreateToken().AdminToken());
        }
        [Authorize]
        [HttpGet("[action]")]
        public IActionResult Deneme()
        {
            return Ok("Hoşgeldiniz");
        }
        [Authorize(Roles = "Admin, Visitor")]
        [HttpGet("[action]")]
        public IActionResult DenemeMi()
        {
            return Ok("Token Başarılı");
        }
    }
}
