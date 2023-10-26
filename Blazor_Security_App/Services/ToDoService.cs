using Blazor_Security_App.Data;
using Blazor_Security_App.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<T> AddAsync(T entity, string userId)
    {
        if (entity is ToDo todo)
        {
            todo.UserId = userId;
            _context.Add(todo);
        }
        else
        {
            _dbSet.Add(entity);
        }
        await _context.SaveChangesAsync();
        return entity;
    }
    public async Task<IEnumerable<T>> GetAllAsync(string userId)
    {
        if (typeof(T) == typeof(ToDo))
        {
            var toDoSet = _context.Set<ToDo>();
            var items = await toDoSet.Where(todo => todo.UserId == userId).ToListAsync();
            return items as IEnumerable<T>;
        }
        throw new InvalidOperationException($"The method {nameof(GetAllAsync)} with a userId parameter is not supported for type {typeof(T).Name}.");
    }


    public async Task<T> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null) return false;

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
