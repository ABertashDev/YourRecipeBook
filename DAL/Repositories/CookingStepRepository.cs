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
    public class CookingStepRepository : CommonRepository<CookingStep>, ICookingStepRepository
    {

        public CookingStepRepository(RecipeBookDbContext context) 
            : base(context)
        {
        }

    }
}
