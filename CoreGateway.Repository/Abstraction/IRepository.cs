using System.Linq.Expressions;

namespace CoreGateway.Repository.Abstraction
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T?> GetFirstOrDefault(Expression<Func<T, bool>> predicate);

        Task Add(T entity);

        Task Delete(T entity);

        Task Update(T entity);

        Task DeleteRange(IEnumerable<T> entities);
    }
}