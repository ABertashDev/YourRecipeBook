using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeCategoryController : Controller
    {
        private IRecipeCategoryService _recipeCategoryService;


        public RecipeCategoryController(IRecipeCategoryService recipeCategoryService)
        {
            _recipeCategoryService = recipeCategoryService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeCategoryModel>>> GetAllAsync() => Ok(await _recipeCategoryService.GetAllAsync());


        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeCategoryModel>> GetByIdAsync(int id)
        {
            RecipeCategoryModel model = await _recipeCategoryService.GetByIdAsync(id);

            if (model is null)
            {
                return NotFound(new { message = $"RecipeCategory with Id={id} was not found" });
            }

            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult<RecipeCategoryModel>> AddAsync([FromBody] RecipeCategoryModel model)
        {
            RecipeCategoryModel createdModel = await _recipeCategoryService.AddAsync(model);
            return Created("", createdModel);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RecipeCategoryModel>> UpdateAsync(int id, [FromBody] RecipeCategoryModel model)
        {
            RecipeCategoryModel existingModel = await _recipeCategoryService.UpdateAsync(id, model);
            return Ok(existingModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _recipeCategoryService.DeleteAsync(id);
            return NoContent();
        }
    }
}
