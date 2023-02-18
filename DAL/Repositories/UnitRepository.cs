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
    public class UnitRepository :CommonRepository<Unit>, IUnitRepository
    {

        public UnitRepository(RecipeBookDbContext context)
            : base(context) 
        {
        }

        public async Task<Unit> GetByName(string name)
        {
            return await dbContext.Set<Unit>().FindAsync(name);
        }
    }
}
