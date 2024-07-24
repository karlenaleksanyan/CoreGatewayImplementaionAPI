namespace CoreGateway.DBMap.Models
{
    public class Suppliers : Entity<Guid>
    {
        public string Name { get; set; }

        public string ContactEmail { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public List<Products> Products { get; set; }
    }
}