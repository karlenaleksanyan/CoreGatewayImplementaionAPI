using CoreGateway.Abstraction;
using CoreGateway.DBMap.Models;
using CoreGateway.Models.Models;
using CoreGateway.Repository.Abstraction;

namespace CoreGateway.BusinessLogic.Services
{
    public class ProductService : Service<ProductModel, Products>, IService<ProductModel>
    {
        private readonly IRepository<Products> _productRepository;

        public ProductService(IRepository<Products> productRepository) : base(productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductModel>?> GetAll()
        {
            IEnumerable<Products> products = await _productRepository.GetAll();

            return products.Select(x => new ProductModel().ToBusinessModel(x));
        }

        public async Task<ProductModel?> GetFirstOrDefault(Guid Id)
        {
            Products product = await _productRepository.GetFirstOrDefault(x => x.Id == Id);

            return new ProductModel().ToBusinessModel(product);
        }
    }
}