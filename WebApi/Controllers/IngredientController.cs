using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredientController : Controller
    {
        private IIngredientService _ingredientService;


        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredientModel>>> GetAllAsync() => Ok(await _ingredientService.GetAllAsync());


        [HttpGet("{id}")]
        public async Task<ActionResult<IngredientModel>> GetByIdAsync(int id)
        {
            IngredientModel model = await _ingredientService.GetByIdAsync(id);

            if (model is null)
            {
                return NotFound(new { message = $"Ingredient with Id={id} was not found" });
            }

            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult<IngredientModel>> AddAsync([FromBody] IngredientModel model)
        {
            IngredientModel createdModel = await _ingredientService.AddAsync(model);
            return Created("", createdModel);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IngredientModel>> UpdateAsync(int id, [FromBody] IngredientModel model)
        {
            IngredientModel existingModel = await _ingredientService.UpdateAsync(id, model);
            return Ok(existingModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _ingredientService.DeleteAsync(id);
            return NoContent();
        }
    }
}
