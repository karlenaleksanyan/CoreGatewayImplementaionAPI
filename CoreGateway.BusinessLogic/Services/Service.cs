using CoreGateway.DBMap.Models;
using CoreGateway.Models.Models;
using CoreGateway.Repository.Abstraction;

namespace CoreGateway.BusinessLogic.Services
{
    public class Service<TModel, TEntity>
            where TModel : BaseModel<TEntity> //, new()
            where TEntity : Entity<Guid>
    {
        private readonly IRepository<TEntity> _repository;

        public Service(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task Add(TModel entity)
        {
            await _repository.Add(entity.ToEntityModel());
        }

        public async Task Delete(Guid Id)
        {
            TEntity entity = await _repository.GetFirstOrDefault(x => x.Id == Id);

            await _repository?.Delete(entity);
        }

        public async Task Update(TModel entity)
        {
            await _repository.Update(entity.ToEntityModel());
        }
    }
}