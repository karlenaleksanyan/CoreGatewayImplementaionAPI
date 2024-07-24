using CoreGateway.DBMap.Models;
using static CoreGateway.Models.Enums.Enum;

namespace CoreGateway.Models.Models
{
    public class TransactionModel
    {
        public Guid TransactionID { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public TransactionType TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }

        public Transactions ToEntityModel()
        {
            return new Transactions
            {
                Id = this.TransactionID,
                ProductId = this.ProductId,
                Quantity = this.Quantity,
                TransactionDate = this.TransactionDate,
                TransactionType = (byte)this.TransactionType,
                CreateDate = DateTime.UtcNow,
            };
        }
    }
}