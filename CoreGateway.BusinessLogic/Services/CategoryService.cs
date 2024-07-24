using CoreGateway.Abstraction;
using CoreGateway.DBMap.Models;
using CoreGateway.Models.Models;
using CoreGateway.Repository.Abstraction;

namespace CoreGateway.BusinessLogic.Services
{
    public class CategoryService :
        Service<CategoryModel, Categories>, IService<CategoryModel>
    {
        private readonly IRepository<Categories> _categoryRepository;

        public CategoryService(IRepository<Categories> categoryRepository) : base(categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryModel>?> GetAll()
        {
            IEnumerable<Categories> categories = await _categoryRepository.GetAll();

            return categories.Select(x => new CategoryModel().ToBusinessModel(x));
        }

        public async Task<CategoryModel?> GetFirstOrDefault(Guid Id)
        {
            Categories category = await _categoryRepository.GetFirstOrDefault(x => x.Id == Id);

            return new CategoryModel().ToBusinessModel(category);
        }
    }
}