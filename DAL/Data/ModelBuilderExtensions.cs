using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public static class ModelBuilderExtensions
    {

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

            //Randomizer.Seed = new Random(5004);

            //// For clear data
            ////var teams = new List<Team>();
            ////var users = new List<User>();
            ////var projects = new List<Project>();
            ////var tasks = new List<BSATask.DAL.Entities.Task>();

            //var teams = GenerateRandomTeams();
            //var users = GenerateRandomUsers(teams);
            //var projects = GenerateRandomProjects(teams, users);
            //var tasks = GenerateRandomTasks(projects, users);

            //modelBuilder.Entity<Project>().HasData(projects);
            //modelBuilder.Entity<BSATask.DAL.Entities.Task>().HasData(tasks);
            //modelBuilder.Entity<Team>().HasData(teams);
            //modelBuilder.Entity<User>().HasData(users);
            //modelBuilder.Entity<TaskStateClass>()
            //                            .HasData(Enum.GetValues(typeof(TaskState))
            //                                .Cast<TaskState>()
            //                                .Select(item => new TaskStateClass
            //                                {
            //                                    Id = ((int)item + 1),
            //                                    Name = Helper.GetTaskStateRepresentation(item)
            //                                })
            //                            );
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

        }

    }
}
