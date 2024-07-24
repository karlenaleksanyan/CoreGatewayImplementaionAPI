using CoreGateway.DBMap.Models;

namespace CoreGateway.Models.Models
{
    public class ProductModel : BaseModel<Products>
    {
        public string Name { get; set; }

        public long Price { get; set; }

        public int StockQuantity { get; set; }

        public string Description { get; set; }

        public Guid CategoryId { get; set; }

        public Guid SuplierId { get; set; }

        public override Products ToEntityModel()
        {
            return new Products
            {
                Id = this.Id,
                Name = this.Name,
                Price = this.Price,
                StockQuantity = this.StockQuantity,
                Description = this.Description,
                CategoryId = this.CategoryId,
                SuplierId = this.SuplierId
            };
        }

        public override ProductModel ToBusinessModel(Products entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Price = entity.Price;
            StockQuantity = entity.StockQuantity;
            Description = entity.Description;
            CategoryId = entity.CategoryId;
            SuplierId = entity.SuplierId;

            return this;
        }
    }
}