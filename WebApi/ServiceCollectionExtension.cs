using BLL.Interfaces;
using BLL;
using BLL.Services;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace CoolParking.WebAPI
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection RegisterRecipeBookServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<ICookingStepService, CookingStepService>();
            services.AddScoped<IRecipeDetailService, RecipeDetailService>();
            services.AddScoped<IRecipeCategoryService, RecipeCategoryService>();
            services.AddScoped<IUnitService, UnitService>();
            services.AddScoped<IIngredientService, IngredientService>();

            services.AddAutoMapper(typeof(AutomapperProfile));

            services.AddDbContext<RecipeBookDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DbConnection")));

            return services;
        }

    }
}
