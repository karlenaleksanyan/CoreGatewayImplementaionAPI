using CoreGateway.Abstraction;
using CoreGateway.DBMap.Models;
using CoreGateway.Models.Models;
using CoreGateway.Repository.Abstraction;

namespace CoreGateway.BusinessLogic.Services
{
    internal class TransactionService : ITransactionService
    {
        private readonly IRepository<Transactions> _transactionRepository;

        public TransactionService(IRepository<Transactions> transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task Add(TransactionModel entity)
        {
            await _transactionRepository.Add(entity.ToEntityModel());
        }
    }
}