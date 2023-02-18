using DAL.Interfaces;
using DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;

namespace DAL.Repositories
{
    public class CommonRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {

        internal RecipeBookDbContext dbContext;

        internal CommonRepository(RecipeBookDbContext context)
        {
            dbContext = context;
        }

        public async Task AddAsync(TEntity entity)
        {
            await dbContext.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            dbContext.Remove(entity); 
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            dbContext.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await dbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public void Update(TEntity entity)
        {
            dbContext.Set<TEntity>().Update(entity);
        }
    }
}
