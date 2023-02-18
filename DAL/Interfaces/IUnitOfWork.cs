using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {

        ICookingStepRepository CookingStepRepository { get; }
        IIngredientRepository IngredientRepository { get; }
        IRecipeCategoryRepository RecipeCategoryRepository { get; }
        IRecipeDetailRepository RecipeDetailRepository { get; }
        IRecipeRepository RecipeRepository { get; }
        IUnitRepository UnitRepository { get; }

        Task SaveAsync();

    }
}
