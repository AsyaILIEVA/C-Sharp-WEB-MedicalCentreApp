using Microsoft.EntityFrameworkCore;

namespace MedicalCentreApp.Data.Repositories
{
    public class BaseRepository<TEntity> where TEntity : class
    {
        protected readonly MedicalCentreAppDbContext dbContext;
        protected readonly DbSet<TEntity> dbSet;

        public BaseRepository(MedicalCentreAppDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> All()
        {
            return dbSet;
        }

        public IQueryable<TEntity> AllAsNoTracking()
        {
            return dbSet.AsNoTracking();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task AddAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            dbSet.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await dbContext.SaveChangesAsync();
        }
    }
}