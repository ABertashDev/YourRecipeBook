using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : Controller
    {
        private IRecipeService _recipeService;


        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeModel>>> GetAllAsync() => Ok(await _recipeService.GetAllAsync());


        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeModel>> GetByIdAsync(int id)
        {
            RecipeModel model = await _recipeService.GetByIdAsync(id);

            if (model is null)
            {
                return NotFound(new { message = $"Recipe with Id={id} was not found" });
            }

            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult<RecipeModel>> AddAsync([FromBody] RecipeModel model)
        {
            RecipeModel createdModel = await _recipeService.AddAsync(model);
            return Created("", createdModel);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RecipeModel>> UpdateAsync(int id, [FromBody] RecipeModel model)
        {
            RecipeModel existingModel = await _recipeService.UpdateAsync(id, model);
            return Ok(existingModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _recipeService.DeleteAsync(id);
            return NoContent();
        }
    }
}
