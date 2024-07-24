using CoreGateway.DBMap;
using CoreGateway.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CoreGateway.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        internal readonly CoreGatewayDbContext _db;
        internal DbSet<T> DbSet { get; private set; }

        public Repository(CoreGatewayDbContext db)
        {
            _db = db;
            DbSet = _db.Set<T>();
        }

        public async Task Add(T entity)
        {
            await DbSet.AddAsync(entity);

            await _db.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            DbSet.Remove(entity);

            await _db.SaveChangesAsync();
        }

        public async Task DeleteRange(IEnumerable<T> entities)
        {
            DbSet.RemoveRange(entities);

            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return DbSet.AsNoTracking();
        }

        public async Task<T?> GetFirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return await DbSet.Where(predicate).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task Update(T entity)
        {
            DbSet.Update(entity);

            await _db.SaveChangesAsync();
        }
    }
}