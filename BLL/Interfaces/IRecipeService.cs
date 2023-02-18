using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IRecipeService : ICrud<RecipeModel>
    {

        Task AddIngredientAsync(int ingredientId, int unitId, double quantity);
        
        Task RemoveIngredientAsync(int ingredientId);

        Task AddCookingStepAsync(int number, string name, string description, string photo);

        Task RemoveCookingStepAsync(int number);

    }
}
