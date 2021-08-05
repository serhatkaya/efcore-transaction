namespace efcore_transaction.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
