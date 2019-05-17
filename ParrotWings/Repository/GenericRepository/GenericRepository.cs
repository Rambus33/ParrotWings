using Microsoft.EntityFrameworkCore;
using ParrotWings.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ParrotWings.Repository
{
    public abstract class GenericRepository<T> : IGenericRepository<T>
        where T : class, new()
    {
        protected RepositoryContext DbContext { get; set; }

        public async Task<T> GetAsync(int id)
        {
            return await DbContext.FindAsync<T>(id);
        }

        public IQueryable<T> Query()
        {
            return DbContext.Set<T>().AsQueryable();
        }

        public void InsertWithoutSaveChanges(T entity)
        {
            DbContext.Set<T>().Add(entity);
        }

        public void UpdateWithoutSaveChanges(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task SaveChangesAsync()
        {
            await DbContext.SaveChangesAsync();
        }
    }
}