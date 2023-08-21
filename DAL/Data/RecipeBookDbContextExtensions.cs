using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Data
{
    public static class RecipeBookDbContextExtensions
    {
        public static async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(this DbSet<TEntity> context) where TEntity : BaseEntity
        {
            return await context.ToListAsync();
        }

        public static async Task<TEntity?> GetByIdAsync<TEntity>(this DbSet<TEntity> context, int id) where TEntity : BaseEntity
        {
            return await context.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }
        public static async Task<TEntity?> GetNotDeletedByIdAsync<TEntity>(this DbSet<TEntity> context, int id) where TEntity : BaseEntity
        {
            return await context.FirstOrDefaultAsync(x => x.Id.Equals(id) && !x.IsDeleted);
        }

        public static async Task<IEnumerable<TEntity?>> GetAllNotDeletedAsync<TEntity>(this DbSet<TEntity> context) where TEntity : BaseEntity
        {
            return await context.Where(x => !x.IsDeleted).ToListAsync();
        }

        public static void Delete<TEntity>(this DbSet<TEntity> context, TEntity? entity, bool isHard) where TEntity : BaseEntity
        {
            if (entity is null)
            {
                return;
            }

            if (isHard)
            {
                context.Remove(entity);
            }
            else
            {
                entity.IsDeleted = true;
            }
        }

        public static async Task DeleteByIdAsync<TEntity>(this DbSet<TEntity> context, int id, bool isHard) where TEntity : BaseEntity
        {
            TEntity? entity = await context.GetByIdAsync(id);
            context.Delete(entity, isHard);
        }

    }
}
