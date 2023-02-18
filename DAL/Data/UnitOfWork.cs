using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    internal class UnitOfWork : IUnitOfWork, IDisposable
    {

        private readonly RecipeBookDbContext db = new RecipeBookDbContext();

        private ICookingStepRepository cookingStepRepository;

        private IIngredientRepository ingredientRepository;

        private IRecipeCategoryRepository recipeCategoryRepository;

        private IRecipeDetailRepository recipeDetailRepository;

        private IRecipeRepository recipeRepository;

        private IUnitRepository unitRepository;

        private bool disposed = false ;


        public ICookingStepRepository CookingStepRepository
        {
            get
            {
                if (cookingStepRepository == null)
                {
                    cookingStepRepository = new CookingStepRepository(db);
                }
                return cookingStepRepository;
            }
        }

        public IIngredientRepository IngredientRepository
        {
            get
            {
                if (ingredientRepository == null)
                {
                    ingredientRepository = new IngredientRepository(db);
                }
                return ingredientRepository;
            }
        }

        public IRecipeCategoryRepository RecipeCategoryRepository
        {
            get
            {
                if (recipeCategoryRepository == null)
                {
                    recipeCategoryRepository = new RecipeCategoryRepository(db);
                }
                return recipeCategoryRepository;
            }
        }

        public IRecipeDetailRepository RecipeDetailRepository
        {
            get
            {
                if (recipeDetailRepository == null)
                {
                    recipeDetailRepository = new RecipeDetailRepository(db);
                }
                return recipeDetailRepository;
            }
        }

        public IRecipeRepository RecipeRepository
        {
            get
            {
                if (recipeRepository == null)
                {
                    recipeRepository = new RecipeRepository(db);
                }
                return recipeRepository;
            }
        }

        public IUnitRepository UnitRepository
        {
            get
            {
                if (unitRepository == null)
                {
                    unitRepository = new UnitRepository(db);
                }
                return unitRepository;
            }
        }


        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
