using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class RecipeRepository : CommonRepository<Recipe>, IRecipeRepository
    {

        public RecipeRepository(RecipeBookDbContext context)
            : base(context) 
        {
        }

        public async Task<IEnumerable<Recipe>> GetAllWithDetailsAsync()
        {
            return await dbContext.Set<Recipe>()
                .Include(x => x.Category)
                .Include(x => x.RecipeDetails)
                    .ThenInclude(rd => rd.Ingredient)
                    .ThenInclude(rd => rd.Unit)
                .Include(x => x.CookingSteps)
                .ToArrayAsync();
        }

        public async Task<Recipe> GetByIdWithDetailsAsync(int id)
        {
            return await dbContext.Set<Recipe>()
                .Include(x => x.Category)
                .Include(x => x.RecipeDetails)
                    .ThenInclude(rd => rd.Ingredient)
                    .ThenInclude(rd => rd.Unit)
                .Include(x => x.CookingSteps)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
