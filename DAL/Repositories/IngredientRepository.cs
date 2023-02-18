using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class IngredientRepository : CommonRepository<Ingredient>, IIngredientRepository
    {

        public IngredientRepository(RecipeBookDbContext context)
            : base(context) 
        {
        }

        public async Task<Ingredient> GetByNameAsync(string name)
        {
            return await dbContext.Set<Ingredient>().FindAsync(name);
        }
    }
}
