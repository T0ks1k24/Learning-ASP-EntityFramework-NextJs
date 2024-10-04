using Entity_Framework.Models.Entitys;

namespace Entity_Framework.Models.Dtos.Orders
{
    public class AddUpdateOrderDto
    {
        public DateTime OrderDate { get; set; }
        public Guid CustomerId { get; set; }
        public CustomerEntity Customer { get; set; }

    }
}
