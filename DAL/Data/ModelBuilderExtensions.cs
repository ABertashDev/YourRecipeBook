using Bogus;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DAL.Data
{
    public static class ModelBuilderExtensions
    {
        private const int NumberOfIngredients = 50;
        private const int NumberOfRecipes = 20;
        private const int NumberOfRecipeDetails = 120;

        private static readonly DateTime _currentDate = DateTime.Now;


        public static void Configure(this ModelBuilder modelBuilder)
        {
            ConfigureCookingSteps(modelBuilder);
            ConfigureIngredients(modelBuilder);
            ConfigureRecipes(modelBuilder);
            ConfigureRecipeCategories(modelBuilder);
            ConfigureRecipeDetails(modelBuilder);
            ConfigureUnits(modelBuilder);
        }

        public static void SeedData(this ModelBuilder modelBuilder)
        {
            var units = GenerateRandomUnits();
            var recipeCategories = GenerateRandomRecipeCategories();
            var ingredients = GenerateRandomIngredients(units);
            var recipes = GenerateRandomRecipe(recipeCategories);
            var cookingSteps = GenerateRandomCookingSteps(recipes);
            var recipeDetails = GenerateRandomRecipeDetails(recipes, ingredients);

            modelBuilder.Entity<Unit>().HasData(units);
            modelBuilder.Entity<RecipeCategory>().HasData(recipeCategories);
            modelBuilder.Entity<Ingredient>().HasData(ingredients);
            modelBuilder.Entity<Recipe>().HasData(recipes);
            modelBuilder.Entity<CookingStep>().HasData(cookingSteps);
            modelBuilder.Entity<RecipeDetail>().HasData(recipeDetails);
        }


        private static void ConfigureCookingSteps(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<CookingStep>()
                .ToTable("CookingSteps");

            modelBuilder
                .Entity<CookingStep>()
                .HasKey(cs => cs.Id);

            modelBuilder
                .Entity<CookingStep>()
                .Property(cs => cs.Name)
                .HasColumnName("Name")
                .HasMaxLength(500)
                .IsRequired();

            modelBuilder
                .Entity<CookingStep>()
                .Property(cs => cs.Number)
                .HasColumnName("Number")
                .IsRequired();

            modelBuilder
                .Entity<CookingStep>()
                .Property(cs => cs.Description)
                .HasColumnName("Description");

            modelBuilder
                .Entity<CookingStep>()
                .Property(cs => cs.Photo)
                .HasColumnName("Photo");

            modelBuilder
                .Entity<CookingStep>()
                .Property(cs => cs.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasDefaultValueSql("getdate()")
                .ValueGeneratedOnAdd();

            modelBuilder
                .Entity<CookingStep>()
                .HasOne(cs => cs.Recipe)
                .WithMany(r => r.CookingSteps)
                .HasForeignKey(cs => cs.RecipeId)
                .HasPrincipalKey(r => r.Id);

        }

        private static void ConfigureIngredients(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Ingredient>()
                .ToTable("Ingredients");

            modelBuilder
                .Entity<Ingredient>()
                .HasKey(i => i.Id);

            modelBuilder
                .Entity<Ingredient>()
                .Property(i => i.Name)
                .HasColumnName("Name")
                .HasMaxLength(500)
                .IsRequired();

            modelBuilder
                .Entity<Ingredient>()
                .Property(i => i.Description)
                .HasColumnName("Description");

            modelBuilder
                .Entity<Ingredient>()
                .Property(i => i.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasDefaultValueSql("getdate()")
                .ValueGeneratedOnAdd();

            modelBuilder
                .Entity<Ingredient>()
                .HasOne(i => i.Unit)
                .WithMany(u => u.Ingredients)
                .HasForeignKey(i => i.UnitId)
                .HasPrincipalKey(u => u.Id);

        }

        private static void ConfigureRecipes(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Recipe>()
                .ToTable("Recipes");

            modelBuilder
                .Entity<Recipe>()
                .HasKey(r => r.Id);

            modelBuilder
                .Entity<Recipe>()
                .Property(r => r.Name)
                .HasColumnName("Name")
                .HasMaxLength(500)
                .IsRequired();

            modelBuilder
                .Entity<Recipe>()
                .Property(r => r.Description)
                .HasColumnName("Description");

            modelBuilder
               .Entity<Recipe>()
               .Property(r => r.CreatedAt)
               .HasColumnName("CreatedAt")
               .HasDefaultValueSql("getdate()")
               .ValueGeneratedOnAdd();

            modelBuilder
                 .Entity<Recipe>()
                .HasOne(r => r.Category)
                .WithMany(c => c.Recipes)
                .HasForeignKey(r => r.CategoryId)
                .HasPrincipalKey(c => c.Id);

        }

        private static void ConfigureRecipeCategories(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<RecipeCategory>()
                .ToTable("RecipeCategories");

            modelBuilder
                .Entity<RecipeCategory>()
                .HasKey(rc => rc.Id);

            modelBuilder
                .Entity<RecipeCategory>()
                .Property(rc => rc.Name)
                .HasColumnName("Name")
                .HasMaxLength(500)
                .IsRequired();

            modelBuilder
               .Entity<RecipeCategory>()
               .Property(rc => rc.CreatedAt)
               .HasColumnName("CreatedAt")
               .HasDefaultValueSql("getdate()")
               .ValueGeneratedOnAdd();

            modelBuilder
                 .Entity<RecipeCategory>()
                .Property(rc => rc.SortOrder)
                .HasColumnName("SortOrder");

        }

        private static void ConfigureRecipeDetails(ModelBuilder modelBuilder)
        {

            modelBuilder
                .Entity<RecipeDetail>()
                .ToTable("RecipeDetails");

            modelBuilder
                .Entity<RecipeDetail>()
                .HasKey(rd => rd.Id);

            modelBuilder
                .Entity<RecipeDetail>()
                .Property(rd => rd.Quantity)
                .HasColumnName("Quantity");

            modelBuilder
              .Entity<RecipeDetail>()
              .Property(rd => rd.CreatedAt)
              .HasColumnName("CreatedAt")
              .HasDefaultValueSql("getdate()")
              .ValueGeneratedOnAdd();

            modelBuilder
                 .Entity<RecipeDetail>()
                .HasOne(rd => rd.Recipe)
                .WithMany(r => r.RecipeDetails)
                .HasForeignKey(rd => rd.RecipeId)
                .HasPrincipalKey(r => r.Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<RecipeDetail>()
                .HasOne(rd => rd.Ingredient)
                .WithMany(i => i.RecipeDetails)
                .HasForeignKey(rd => rd.IngredientId)
                .HasPrincipalKey(i => i.Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<RecipeDetail>()
                .HasOne(rd => rd.Unit)
                .WithMany(u => u.RecipeDetails)
                .HasForeignKey(rd => rd.UnitId)
                .HasPrincipalKey(i => i.Id)
                .OnDelete(DeleteBehavior.NoAction);

        }

        private static void ConfigureUnits(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Unit>()
                .ToTable("Units");

            modelBuilder
                .Entity<Unit>()
                .HasKey(u => u.Id);

            modelBuilder
                .Entity<Unit>()
                .Property(u => u.Name)
                .HasColumnName("Name")
                .HasMaxLength(500)
                .IsRequired();

            modelBuilder
              .Entity<Unit>()
              .Property(u => u.CreatedAt)
              .HasColumnName("CreatedAt")
              .HasDefaultValueSql("getdate()")
              .ValueGeneratedOnAdd();
        }


        private static ICollection<Unit> GenerateRandomUnits()
        {
            int unitId = 1;

            List<Unit> generatedUnits = new()
            {
                // Volume
                new Unit() { Id = unitId++, Name = "Milliliter", Abbreviation = "ml", CreatedAt = _currentDate },
                new Unit() { Id = unitId++, Name = "Liter", Abbreviation = "l", CreatedAt = _currentDate },
                new Unit() { Id = unitId++, Name = "Teaspoon", Abbreviation = "tsp.", CreatedAt = _currentDate },
                new Unit() { Id = unitId++, Name = "Tablespoon", Abbreviation = "tbsp.", CreatedAt = _currentDate },
                new Unit() { Id = unitId++, Name = "Cup", Abbreviation = "c", CreatedAt = _currentDate },

                // Weight
                new Unit() { Id = unitId++, Name = "Milligram", Abbreviation = "mg", CreatedAt = _currentDate },
                new Unit() { Id = unitId++, Name = "Gram", Abbreviation = "g", CreatedAt = _currentDate },
                new Unit() { Id = unitId++, Name = "Kilogram", Abbreviation = "kg", CreatedAt = _currentDate },

                // Temperature
                new Unit() { Id = unitId++, Name = "Degree celsius", Abbreviation = "°C", CreatedAt = _currentDate }
            };

            return generatedUnits;
        }

        private static ICollection<RecipeCategory> GenerateRandomRecipeCategories()
        {
            int categoryId = 1;

            List<RecipeCategory> generatedRecipeCategories = new()
            {
                new RecipeCategory() { Id = categoryId++, Name = "Snacks", SortOrder = 10, CreatedAt = _currentDate },
                new RecipeCategory() { Id = categoryId++, Name = "Salads", SortOrder = 20, CreatedAt = _currentDate },
                new RecipeCategory() { Id = categoryId++, Name = "First dishes", SortOrder = 30, CreatedAt = _currentDate },
                new RecipeCategory() { Id = categoryId++, Name = "Second dishes", SortOrder = 40, CreatedAt = _currentDate },
                new RecipeCategory() { Id = categoryId++, Name = "Meat dishes", SortOrder = 50, CreatedAt = _currentDate },
                new RecipeCategory() { Id = categoryId++, Name = "Fish dishes", SortOrder = 60, CreatedAt = _currentDate },
                new RecipeCategory() { Id = categoryId++, Name = "Baking / desserts", SortOrder = 70, CreatedAt = _currentDate },
                new RecipeCategory() { Id = categoryId++, Name = "Beverages", SortOrder = 80, CreatedAt = _currentDate }
            };

            return generatedRecipeCategories;
        }

        private static ICollection<Ingredient> GenerateRandomIngredients(ICollection<Unit> units)
        {
            int ingId = 1;

            var faker = new Faker<Ingredient>()
                .RuleFor(u => u.Id, f => ingId++)
                .RuleFor(u => u.Name, f => f.Commerce.Product())
                .RuleFor(u => u.Description, f => f.Commerce.ProductDescription())
                .RuleFor(u => u.UnitId, f => f.PickRandom(units).Id)
                .RuleFor(u => u.CreatedAt, f => _currentDate);

            var generatedIngredients = faker.Generate(NumberOfIngredients);

            return generatedIngredients;
        }

        private static ICollection<Recipe> GenerateRandomRecipe(ICollection<RecipeCategory> recipeCategories)
        {
            int recipeId = 1;

            var faker = new Faker<Recipe>()
                .RuleFor(r => r.Id, f => recipeId++)
                .RuleFor(r => r.Name, f => f.Lorem.Sentence(1, 5))
                .RuleFor(r => r.Description, f => f.Lorem.Sentence(10, 20))
                .RuleFor(r => r.CategoryId, f => f.PickRandom(recipeCategories).Id)
                .RuleFor(r => r.CreatedAt, f => _currentDate);

            var generatedRecipes = faker.Generate(NumberOfRecipes);

            return generatedRecipes;
        }

        private static ICollection<CookingStep> GenerateRandomCookingSteps(ICollection<Recipe> recipes)
        {
            int csId = 1;

            List<CookingStep> generatedCookingSteps = new();

            foreach (var recipe in recipes)
            {
                int number = 1;

                var faker = new Faker<CookingStep>()
                    .RuleFor(cs => cs.Id, f => csId++)
                    .RuleFor(cs => cs.Name, f => f.Lorem.Sentence(10, 20))
                    .RuleFor(cs => cs.Number, f => number++)
                    .RuleFor(cs => cs.Description, f => f.Lorem.Paragraph())
                    .RuleFor(cs => cs.Photo, f => f.Image.PicsumUrl())
                    .RuleFor(cs => cs.RecipeId, f => f.PickRandom(recipes).Id)
                    .RuleFor(cs => cs.CreatedAt, f => _currentDate);

                generatedCookingSteps.AddRange(faker.Generate(new Random().Next(5, 12)));
            }

            return generatedCookingSteps;
        }

        private static ICollection<RecipeDetail> GenerateRandomRecipeDetails(ICollection<Recipe> recipes, ICollection<Ingredient> ingredients)
        {
            int rdId = 1;

            var faker = new Faker<RecipeDetail>()
                .RuleFor(rd => rd.Id, f => rdId++)
                .RuleFor(rd => rd.RecipeId, f => f.PickRandom(recipes).Id)
                .RuleFor(rd => rd.Ingredient, f => f.PickRandom(ingredients))
                .RuleFor(rd => rd.IngredientId, (f, rd) => rd.Ingredient.Id)
                .RuleFor(rd => rd.UnitId, (f, rd) => rd.Ingredient.Unit.Id)
                .RuleFor(rd => rd.Quantity, f => f.PickRandom<double>())
                .RuleFor(rd => rd.CreatedAt, f => _currentDate);

            var generatedRecipeDetails = faker.Generate(NumberOfRecipeDetails);

            return generatedRecipeDetails;
        }
    }
}
