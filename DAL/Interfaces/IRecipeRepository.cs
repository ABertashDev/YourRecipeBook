using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    internal interface IRecipeRepository : IRepository<Recipe>
    {

        Task<IEnumerable<Recipe>> GetAllWithDetailsAsync();

        Task<Recipe> GetByIdWithDetailsAsync(int id);

    }
}
