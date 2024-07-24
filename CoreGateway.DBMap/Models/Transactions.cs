using System.ComponentModel.DataAnnotations.Schema;

namespace CoreGateway.DBMap.Models
{
    public class Transactions : Entity<Guid>
    {
        public DateTime TransactionDate { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public byte TransactionType { get; set; }

        public Guid ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Products Product { get; set; }
    }
}