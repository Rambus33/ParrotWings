using Microsoft.EntityFrameworkCore;
using ParrotWings.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParrotWings.Services
{
    public abstract class GenericService<T> : IGenericService<T> where T : class, new()
    {
        protected IGenericRepository<T> EntityRepository { get; }

        protected GenericService(IGenericRepository<T> entityRepository)
        {
            EntityRepository = entityRepository;
        }

        public void InsertWithoutSaveChanges(T entity)
        {
            EntityRepository.InsertWithoutSaveChanges(entity);
        }

        public void UpdateWithoutSaveChanges(T entity)
        {
            EntityRepository.UpdateWithoutSaveChanges(entity);
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await EntityRepository.Query().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await EntityRepository.GetAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await EntityRepository.SaveChangesAsync();
        }
    }
}