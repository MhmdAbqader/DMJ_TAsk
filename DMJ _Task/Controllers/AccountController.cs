using DMJ_Application.Dtos;
using DMJ_Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DMJ__Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authSevice;

        public AccountController(IAuthenticationService authSevice)
        {
            _authSevice = authSevice;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var result = await _authSevice.Register(registerDto);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);
            
            //return Ok(new { isauth = result.IsAuthenticated, Token = result.Token, expire = result.ExpireOn });
            return Ok(result);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authSevice.Login(loginDto);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);          

            return Ok(result);
        }
    }
}
