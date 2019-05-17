using System.Linq;
using System.Threading.Tasks;

namespace ParrotWings.Repository
{
    public interface IGenericRepository<T>
    {
        Task<T> GetAsync(int id);

        IQueryable<T> Query();

        void InsertWithoutSaveChanges(T entity);

        void UpdateWithoutSaveChanges(T entity);

        Task SaveChangesAsync();
    }
}