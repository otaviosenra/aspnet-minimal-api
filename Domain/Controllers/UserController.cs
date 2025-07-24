using Microsoft.AspNetCore.Mvc;
using MinimalApi.Domain.DTOs;
using MinimalApi.Domain.Entities;
using MinimalApi.Domain.Enums;
using MinimalApi.Domain.Interfaces;

namespace AspNetMinimalApi.Domain.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }



        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetUsers(int page = 1, int pageSize = 10)
        {
            List<User> users = await _service.GetAllUsers(page, pageSize);
            List<UserDTO> dtos = users.Select(c => new UserDTO { Email = c.Email, Name = c.Name, Profile = Enum.Parse<ProfileType>(c.Profile), Password = c.Password }).ToList();

            return Ok(dtos);
        }

  
  


        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            try
            {
                User? user = await _service.GetUserById(id);
            
                UserDTO dto = new UserDTO
                {
                    Name = user!.Name,
                    Email = user!.Email,
                    Password = user!.Password,
                    Profile = Enum.Parse<ProfileType>(user!.Profile),
                };

                return Ok(dto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Queijo com ID {id} não encontrado." });
            }

        }





        [HttpPost]
        public async Task<ActionResult<UserDTO>> CreateUser([FromBody] UserDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
                Profile = dto.Profile.ToString()
            };

            await _service.CreateUser(user);

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, dto);
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                User userEntity = new User
                {
                    Id = id,
                    Name = dto.Name,
                    Email = dto.Email,
                    Password = dto.Password,
                    Profile = dto.Profile.ToString()
                };

                await _service.UpdateUser(userEntity);
                return Ok(new { message = $"Usuário '{dto.Name}' atualizado com sucesso." });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Usuário com ID {id} não encontrado." });
            }

        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                User? user = await _service.GetUserById(id);
                await _service.DeleteUser(id);
                return Ok(new { message = $"Usuário '{user!.Name}' removido com sucesso." });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Usuário com ID {id} não encontrado." });
            }


        }
    }
}