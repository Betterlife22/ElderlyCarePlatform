using System.Linq.Expressions;

namespace DAL.Interfaces;

public interface IGenericRepository<T> where T : class
{
    // query
    IQueryable<T> Entities { get; }

    // non async
    IEnumerable<T> GetAll();
    T? GetById(object id);
    void Insert(T obj);
    void InsertRange(IList<T> obj);
    void Update(T obj);
    void Delete(object id);
    void Save();
    void DeleteRange(IEnumerable<T> entities);

    // async
    //Task<List<T>> GetAllAsync();
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
    Task<T> GetByPropertyAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true, string? includeProperties = null);
    Task<T?> GetByIdAsync(object id);
    Task InsertAsync(T obj);
    Task UpdateAsync(T obj);
    Task DeleteAsync(object id);
    Task DeleteAsync(params object[] keyValues);
    Task SaveAsync();
    Task<T?> FindAsync(params object[] keyValues);
    Task<int> CountAsync();
    Task<IList<T>> SearchAsync(Expression<Func<T, bool>> filter);

}
