using Microsoft.EntityFrameworkCore;
using DAL.Entities;
using System.Reflection.Emit;

namespace DAL.Data
{
    public class RecipeBookDbContext : DbContext
    {

        //public RecipeBookDbContext(DbContextOptions<RecipeBookDbContext> options) : base(options) { }

        public RecipeBookDbContext() { }

        public DbSet<CookingStep> CookingSteps { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeCategory> RecipeCategories { get; set; }
        public DbSet<RecipeDetail> RecipeDetails { get; set; }
        public DbSet<Unit> Units { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ReceiptBookTest;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            OnModelCreating_CookingStep(modelBuilder);
            OnModelCreating_Ingredient(modelBuilder);
            OnModelCreating_Recipe(modelBuilder);
            OnModelCreating_RecipeCategory(modelBuilder);
            OnModelCreating_RecipeDetail(modelBuilder);
            OnModelCreating_Unit(modelBuilder);

        }

        protected void OnModelCreating_CookingStep(ModelBuilder modelBuilder)
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

        protected void OnModelCreating_Ingredient(ModelBuilder modelBuilder)
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

        protected void OnModelCreating_Recipe(ModelBuilder modelBuilder)
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

        protected void OnModelCreating_RecipeCategory(ModelBuilder modelBuilder)
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

        protected void OnModelCreating_RecipeDetail(ModelBuilder modelBuilder)
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

        protected void OnModelCreating_Unit(ModelBuilder modelBuilder)
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
