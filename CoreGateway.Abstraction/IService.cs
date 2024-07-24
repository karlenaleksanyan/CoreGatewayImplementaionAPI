namespace CoreGateway.Abstraction
{
    public interface IService<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T?> GetFirstOrDefault(Guid Id);

        Task Add(T entity);

        Task Update(T entity);

        Task Delete(Guid Id);
    }
}