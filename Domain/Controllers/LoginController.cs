using Microsoft.AspNetCore.Mvc;
using MinimalApi.Domain.DTOs;
using MinimalApi.Domain.Services;

namespace AspNetMinimalApi.Domain.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {

        private readonly LoginService _service;

        public LoginController(LoginService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetLogin ([FromBody] LoginDTO loginDTO)
        {
            try
            {
                if (await _service.Login(loginDTO))
                    return Ok("Login successful");
                else
                    return Unauthorized(new { message = "Invalid username or password" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request.", error = ex.Message });
            }
        }
    }
}