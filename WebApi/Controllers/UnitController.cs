using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnitController : Controller
    {
        
        private IUnitService _unitService;


        public UnitController(IUnitService unitService)
        {
            _unitService = unitService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnitModel>>> GetAllAsync() => Ok(await _unitService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<UnitModel>> GetByIdAsync(int id)
        {
            UnitModel model = await _unitService.GetByIdAsync(id);

            if (model is null)
            {
                return NotFound(new { message = $"Unit with Id={id} was not found" });
            }

            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult<UnitModel>> AddAsync([FromBody] UnitModel model)
        {
            UnitModel createdModel = await _unitService.AddAsync(model);
            return Created("", createdModel);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UnitModel>> UpdateAsync(int id, [FromBody] UnitModel model)
        {
            UnitModel existingModel = await _unitService.UpdateAsync(id, model);
            return Ok(existingModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _unitService.DeleteAsync(id);
            return NoContent();
        }
    }
}
