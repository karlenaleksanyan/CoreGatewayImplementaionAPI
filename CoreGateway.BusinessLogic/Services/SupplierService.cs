using CoreGateway.Abstraction;
using CoreGateway.DBMap.Models;
using CoreGateway.Models.Models;
using CoreGateway.Repository.Abstraction;

namespace CoreGateway.BusinessLogic.Services
{
    internal class SupplierService :
         Service<SupplierModel, Suppliers>, IService<SupplierModel>
    {
        private readonly IRepository<Suppliers> _supplierRepository;

        public SupplierService(IRepository<Suppliers> supplierRepository) : base(supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<IEnumerable<SupplierModel>> GetAll()
        {
            IEnumerable<Suppliers> suppliers = await _supplierRepository.GetAll();

            return suppliers.Select(x => new SupplierModel().ToBusinessModel(x));
        }

        public async Task<SupplierModel?> GetFirstOrDefault(Guid Id)
        {
            Suppliers supplier = await _supplierRepository.GetFirstOrDefault(x => x.Id == Id);

            return new SupplierModel().ToBusinessModel(supplier);
        }
    }
}