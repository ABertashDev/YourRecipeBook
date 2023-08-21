using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeDetailController : Controller
    {
        private IRecipeDetailService _recipeDetailService;


        public RecipeDetailController(IRecipeDetailService recipeDetailService)
        {
            _recipeDetailService = recipeDetailService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeDetailModel>>> GetAllAsync() => Ok(await _recipeDetailService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeDetailModel>> GetByIdAsync(int id)
        {
            RecipeDetailModel model = await _recipeDetailService.GetByIdAsync(id);

            if (model is null)
            {
                return NotFound(new { message = $"RecipeDetail with Id={id} was not found" });
            }

            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult<RecipeDetailModel>> AddAsync([FromBody] RecipeDetailModel model)
        {
            RecipeDetailModel createdModel = await _recipeDetailService.AddAsync(model);
            return Created("", createdModel);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RecipeDetailModel>> UpdateAsync(int id, [FromBody] RecipeDetailModel model)
        {
            RecipeDetailModel existingModel = await _recipeDetailService.UpdateAsync(id, model);
            return Ok(existingModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _recipeDetailService.DeleteAsync(id);
            return NoContent();
        }
    }
}
