using CoreGateway.Models.Models;

namespace CoreGateway.Abstraction
{
    public interface ITransactionService
    {
        Task Add(TransactionModel entity);
    }
}