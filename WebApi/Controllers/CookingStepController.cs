using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CookingStepController : Controller
    {
        private ICookingStepService _cookingStepService;


        public CookingStepController(ICookingStepService cookingStepService)
        {
            _cookingStepService = cookingStepService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CookingStepModel>>> GetAllAsync() => Ok(await _cookingStepService.GetAllAsync());


        [HttpGet("{id}")]
        public async Task<ActionResult<CookingStepModel>> GetByIdAsync(int id)
        {
            CookingStepModel model = await _cookingStepService.GetByIdAsync(id);

            if (model is null)
            {
                return NotFound(new { message = $"CookingStep with Id={id} was not found" });
            }

            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult<CookingStepModel>> AddAsync([FromBody] CookingStepModel model)
        {
            CookingStepModel createdModel = await _cookingStepService.AddAsync(model);
            return Created("", createdModel);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CookingStepModel>> UpdateAsync(int id, [FromBody] CookingStepModel model)
        {
            CookingStepModel existingModel = await _cookingStepService.UpdateAsync(id, model);
            return Ok(existingModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _cookingStepService.DeleteAsync(id);
            return NoContent();
        }
    }
}
