using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Domain.Entities;
using MinimalApi.Domain.Services;

namespace AspNetMinimalApi.Domain.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheeseController : ControllerBase
    {
        private readonly CheeseService _service;

        public CheeseController(CheeseService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cheese>>> GetCheeses(int page = 1, int pageSize = 10)
        {
            var cheeses = await _service.GetAllCheeses(page, pageSize);
            return Ok(cheeses);
        }

  
  


        [HttpGet("{id}")]
        public async Task<ActionResult<Cheese>> GetCheese(int id)
        {
            var cheese = await _service.GetCheeseById(id);

            if (cheese == null)
            {
                return NotFound(new { message = $"Queijo com ID {id} não encontrado." });
            }

            return Ok(cheese);
        }





        [HttpPost]
        public async Task<ActionResult<Cheese>> CreateCheese([FromBody] Cheese cheese)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _service.CreateCheese(cheese);

            return CreatedAtAction(nameof(GetCheese), new { id = cheese.Id }, cheese);
        }




        // [HttpPut("{id}")]
        // public async Task<IActionResult> UpdateCheese(int id, [FromBody] Cheese cheese)
        // {
        //     if (id != cheese.Id)
        //     {
        //         return BadRequest(new { message = "ID do parâmetro não confere com ID do objeto." });
        //     }

        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }

        //     var existingCheese = await _service.Cheeses.FindAsync(id);
        //     if (existingCheese == null)
        //     {
        //         return NotFound(new { message = $"Queijo com ID {id} não encontrado." });
        //     }

        //     existingCheese.Type = cheese.Type;
        //     existingCheese.Quantity = cheese.Quantity;
        //     existingCheese.Price = cheese.Price;

        //     try
        //     {
        //         await _service.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!CheeseExists(id))
        //         {
        //             return NotFound();
        //         }
        //         throw;
        //     }

        //     return Ok(existingCheese);
        // }



        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteCheese(int id)
        // {
        //     var cheese = await _service.Cheeses.FindAsync(id);
        //     if (cheese == null)
        //     {
        //         return NotFound(new { message = $"Queijo com ID {id} não encontrado." });
        //     }

        //     _service.Cheeses.Remove(cheese);
        //     await _service.SaveChangesAsync();

        //     return Ok(new { message = $"Queijo '{cheese.Type}' removido com sucesso." });
        // }

       


        // [HttpGet("search")]
        // public async Task<ActionResult<IEnumerable<Cheese>>> SearchCheeses([FromQuery] string name)
        // {
        //     if (string.IsNullOrWhiteSpace(name))
        //     {
        //         return BadRequest(new { message = "Parâmetro 'name' é obrigatório." });
        //     }

        //     var cheeses = await _service.Cheeses
        //         .Where(c => c.Type.Contains(name))
        //         .ToListAsync();

        //     return Ok(cheeses);
        // }



        // [HttpGet("type/{type}")]
        // public async Task<ActionResult<IEnumerable<Cheese>>> GetCheesesByType(string type)
        // {
        //     var cheeses = await _service.Cheeses
        //         .Where(c => c.Type.ToLower() == type.ToLower())
        //         .ToListAsync();

        //     return Ok(cheeses);
        // }

        // private bool CheeseExists(int id)
        // {
        //     return _service.Cheeses.Any(e => e.Id == id);
        // }
    }
}