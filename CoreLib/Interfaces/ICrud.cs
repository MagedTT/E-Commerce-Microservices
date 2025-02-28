using System.Linq.Expressions;

namespace CoreLib.Interfaces;

public interface ICrud<T> where T : class
{
    Task<T> GetByIdAsync(int id); // FindByIdAsync
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByAsync(Expression<Func<T, bool>> predicate);
    Task<ResponseMessage> CreateAsync(T entity);
    Task<ResponseMessage> UpdateAsync(T entity);
    Task<ResponseMessage> DeleteAsync(T entity);
}