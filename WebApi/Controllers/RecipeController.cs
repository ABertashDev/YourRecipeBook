using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
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


    }
}
