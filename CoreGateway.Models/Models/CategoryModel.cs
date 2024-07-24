using CoreGateway.DBMap.Models;

namespace CoreGateway.Models.Models
{
    public class CategoryModel : BaseModel<Categories>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public override Categories ToEntityModel()
        {
            return new Categories
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                CreateDate = DateTime.UtcNow,
            };
        }

        public override CategoryModel ToBusinessModel(Categories entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;

            return this;
        }
    }
}