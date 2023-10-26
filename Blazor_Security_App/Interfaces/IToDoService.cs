namespace Blazor_Security_App.Interfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync(string userId);
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity, string userId);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
    }
}

