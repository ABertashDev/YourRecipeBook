using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    internal interface IRecipeCategoryRepository : IRepository<RecipeCategory>
    {

        Task<RecipeCategory> GetByNameAsync(string name);

    }
}
