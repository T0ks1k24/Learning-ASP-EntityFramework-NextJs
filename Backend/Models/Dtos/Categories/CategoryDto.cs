using Entity_Framework.Models.Entitys;

namespace Entity_Framework.Models.Dtos.Categories
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProductEntity> Products { get; set; }
    }
}
