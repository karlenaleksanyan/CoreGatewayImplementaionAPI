namespace CoreGateway.DBMap.Models
{
    public abstract class Entity<T> where T : struct, IEquatable<T>
    {
        public T Id { get; set; }
        public bool IsActive { get; set; } = true;

        public long? CreateUser { get; set; }

        public DateTime CreateDate { get; set; }

        public long? ModifyUser { get; set; }

        public DateTime? ModifyDate { get; set; }
    }
}