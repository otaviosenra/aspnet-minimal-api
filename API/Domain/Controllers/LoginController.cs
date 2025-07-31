using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Domain.DTOs;
using MinimalApi.Domain.Interfaces;

namespace MinimalApi.Domain.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class LoginController : ControllerBase
{

    private readonly ILoginService _service;


    public LoginController(ILoginService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> PostLogin([FromBody] LoginDTO loginDTO)
    {
        try
        {
            if (await _service.Login(loginDTO))
            {
                UsuarioLogado user = _service.LogarUsuario(loginDTO);
                return Ok(user);
            }
            else
                return Unauthorized(new { message = "Invalid username or password" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while processing your request.", error = ex.Message });
        }
    }
}
