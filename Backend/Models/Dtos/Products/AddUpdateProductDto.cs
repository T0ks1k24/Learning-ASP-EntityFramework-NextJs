using Entity_Framework.Models.Dtos.Categories;

namespace Entity_Framework.Models.Dtos.Products
{
    public class AddUpdateProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public Guid CategoryId { get; set; }
    }
}
