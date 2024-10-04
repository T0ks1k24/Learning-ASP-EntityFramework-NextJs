﻿namespace Entity_Framework.Models.Entitys
{
    public class OrderItemEntity
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public OrderEntity Order { get; set; }
        public Guid ProductId { get; set; }
        public ProductEntity Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

    }
}
