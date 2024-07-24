using CoreGateway.DBMap.Models;

namespace CoreGateway.Models.Models
{
    public class SupplierModel : BaseModel<Suppliers>
    {
        public string Name { get; set; }

        public string ContactEmail { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public override Suppliers ToEntityModel()
        {
            return new Suppliers
            {
                Id = this.Id,
                Name = this.Name,
                Address = this.Address,
                ContactEmail = this.ContactEmail,
                PhoneNumber = this.PhoneNumber,
                CreateDate = DateTime.UtcNow,
            };
        }

        public override SupplierModel ToBusinessModel(Suppliers entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            ContactEmail = entity.ContactEmail;
            PhoneNumber = entity.PhoneNumber;
            Address = entity.Address;

            return this;
        }
    }
}