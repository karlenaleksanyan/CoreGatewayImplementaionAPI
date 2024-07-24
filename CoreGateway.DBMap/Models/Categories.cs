namespace CoreGateway.DBMap.Models
{
    public class Categories : Entity<Guid>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<Products> Products { get; set; }
    }
}