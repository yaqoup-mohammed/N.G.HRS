using N.G.HRS.Areas.Employees.Models;

namespace N.G.HRS.Repository
{
    public interface IRepository<T> where T : class
    {

        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int? id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int? Id);
        Employee GetEmployee(int v);
    }
}
