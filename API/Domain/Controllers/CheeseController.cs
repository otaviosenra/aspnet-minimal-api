using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Domain.DTOs;
using MinimalApi.Domain.Entities;
using MinimalApi.Domain.Interfaces;

namespace MinimalApi.Domain.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "ADMIN,USER")]
public class CheeseController : ControllerBase
{
    private readonly ICheeseService _service;

    public CheeseController(ICheeseService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<CheeseDTO>>> GetCheeses(int page = 1, int pageSize = 10)
    {
        List<Cheese> cheeses = await _service.GetAllCheeses(page, pageSize);
        List<CheeseDTO> dtos = cheeses.Select(c => new CheeseDTO { Type = c.Type, Quantity = c.Quantity, Price = c.Price }).ToList();

        return Ok(dtos);
    }



    [HttpGet("{id}")]
    public async Task<ActionResult<CheeseDTO>> GetCheese(int id)
    {
        try
        {
            Cheese? cheese = await _service.GetCheeseById(id);

            CheeseDTO dto = new CheeseDTO
            {
                Type = cheese!.Type,
                Quantity = cheese!.Quantity,
                Price = cheese!.Price
            };

            return Ok(dto);
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = $"Queijo com ID {id} não encontrado." });
        }

    }


    [HttpPost]
    public async Task<ActionResult<CheeseDTO>> CreateCheese([FromBody] CheeseDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Cheese cheese = new Cheese
        {
            Type = dto.Type,
            Quantity = dto.Quantity,
            Price = dto.Price
        };

        await _service.CreateCheese(cheese);

        return CreatedAtAction(nameof(GetCheese), new { id = cheese.Id }, dto);
    }




    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCheese(int id, [FromBody] CheeseDTO dto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Cheese cheeseEntity = new Cheese
            {
                Id = id,
                Type = dto.Type,
                Quantity = dto.Quantity,
                Price = dto.Price
            };

            await _service.UpdateCheese(cheeseEntity);
            return Ok(new { message = $"Queijo '{dto.Type}' atualizado com sucesso." });
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = $"Queijo com ID {id} não encontrado." });
        }

    }



    // [HttpDelete("{id}")]
    // public async Task<IActionResult> DeleteCheese(int id)
    // {
    //     try
    //     {
    //         Cheese? cheese = await _service.GetCheeseById(id);
    //         await _service.DeleteCheese(id);
    //         return Ok(new { message = $"Queijo '{cheese!.Type}' removido com sucesso." });
    //     }
    //     catch (KeyNotFoundException)
    //     {
    //         return NotFound(new { message = $"Queijo com ID {id} não encontrado." });
    //     }


    // }


}
