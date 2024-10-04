namespace Entity_Framework.Models.Entitys
{
    public class OrderEntity
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid CustomerId { get; set; }

        public CustomerEntity Customer { get; set; }
        public List<OrderItemEntity> OrderItems { get; set; }
    }
}
