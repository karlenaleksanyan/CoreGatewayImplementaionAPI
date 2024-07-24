namespace CoreGateway.Models.Models
{
    public abstract class BaseModel<T> where T : class
    {
        public Guid Id { get; set; }

        public abstract T ToEntityModel();

        public abstract BaseModel<T> ToBusinessModel(T entity);
    }
}