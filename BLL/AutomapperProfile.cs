using AutoMapper;
using BLL.Models;
using DAL.Entities;

namespace BLL
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Recipe, RecipeModel>()
                .ForMember(rm => rm.Id, r => r.MapFrom(x => x.Id))
                .ForMember(rm => rm.Name, r => r.MapFrom(x => x.Name))
                .ForMember(rm => rm.Description, r => r.MapFrom(x => x.Description))
                .ForMember(rm => rm.CategoryId, r => r.MapFrom(x => x.CategoryId))
                .ForMember(rm => rm.CategoryName, r => r.MapFrom(x => x.Category.Name))
                .ForMember(rm => rm.RecipeDetails, r => r.MapFrom(x => x.RecipeDetails.Where(rd => !rd.IsDeleted)))
                .ReverseMap();

            CreateMap<RecipeCategory, RecipeCategoryModel>()
                .ForMember(rcm => rcm.Id, rc => rc.MapFrom(x => x.Id))
                .ForMember(rcm => rcm.Name, rc => rc.MapFrom(x => x.Name))
                .ForMember(rcm => rcm.SortOrder, rc => rc.MapFrom(x => x.SortOrder))
                .ForMember(rcm => rcm.Recipes, rc => rc.MapFrom(x => x.Recipes.Where(rc => !rc.IsDeleted)))
                .ReverseMap();

            CreateMap<RecipeDetail, RecipeDetailModel>()
                .ForMember(rdm => rdm.Id, rd => rd.MapFrom(x => x.Id))
                .ForMember(rdm => rdm.Quantity, rd => rd.MapFrom(x => x.Quantity))
                .ForMember(rdm => rdm.RecipeId, rd => rd.MapFrom(x => x.RecipeId))
                .ForMember(rdm => rdm.IngredientId, rd => rd.MapFrom(x => x.IngredientId))
                .ForMember(rdm => rdm.IngredientName, rd => rd.MapFrom(x => x.Ingredient.Name))
                .ForMember(rdm => rdm.UnitId, rd => rd.MapFrom(x => x.UnitId))
                .ForMember(rdm => rdm.UnitName, rd => rd.MapFrom(x => x.Unit.Name))
                .ReverseMap();

            CreateMap<CookingStep, CookingStepModel>()
                .ForMember(csm => csm.Id, cs => cs.MapFrom(x => x.Id))
                .ForMember(csm => csm.Number, cs => cs.MapFrom(x => x.Number))
                .ForMember(csm => csm.Name, cs => cs.MapFrom(x => x.Name))
                .ForMember(csm => csm.Description, cs => cs.MapFrom(x => x.Description))
                .ForMember(csm => csm.Photo, cs => cs.MapFrom(x => x.Photo))
                .ForMember(csm => csm.RecipeId, cs => cs.MapFrom(x => x.RecipeId))
                .ReverseMap();

            CreateMap<Ingredient, IngredientModel>()
                .ForMember(im => im.Id, i => i.MapFrom(x => x.Id))
                .ForMember(im => im.Name, i => i.MapFrom(x => x.Name))
                .ForMember(im => im.Description, i => i.MapFrom(x => x.Description))
                .ForMember(im => im.UnitId, i => i.MapFrom(x => x.UnitId))
                .ForMember(im => im.UnitName, i => i.MapFrom(x => x.Unit.Name))
                .ReverseMap();

            CreateMap<Unit, UnitModel>()
                .ForMember(um => um.Id, u => u.MapFrom(x => x.Id))
                .ForMember(um => um.Name, u => u.MapFrom(x => x.Name))
                .ForMember(um => um.Abbreviation, u => u.MapFrom(x => x.Abbreviation))
                .ReverseMap();
        }
    }
}