using System.ComponentModel.DataAnnotations.Schema;

namespace CoreGateway.DBMap.Models
{
    public class Products : Entity<Guid>
    {
        public string Name { get; set; }

        public long Price { get; set; }

        public int StockQuantity { get; set; }

        public string Description { get; set; }

        public Guid CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Categories? Category { get; set; }

        public Guid SuplierId { get; set; }

        [ForeignKey("SuplierId")]
        public Suppliers? Suplier { get; set; }
    }
}