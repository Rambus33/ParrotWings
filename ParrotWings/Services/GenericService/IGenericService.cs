using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParrotWings.Services
{
    public interface IGenericService<T>
    {
        Task<IList<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        void InsertWithoutSaveChanges(T entity);

        void UpdateWithoutSaveChanges(T entity);

        Task SaveChangesAsync();
    }
}