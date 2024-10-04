namespace Entity_Framework.Models.Entitys
{
    public class CategoryEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<ProductEntity> Products { get; set; }
    }
}
