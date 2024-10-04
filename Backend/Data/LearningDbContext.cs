using Entity_Framework.Models.Entitys;
using Microsoft.EntityFrameworkCore;

namespace Entity_Framework.Data
{
    public class LearningDbContext(DbContextOptions<LearningDbContext> options) : DbContext(options)
    {
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<OrderItemEntity> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Налаштування зв'язків та обмежень
            modelBuilder.Entity<OrderItemEntity>()
                .HasKey(oi => new { oi.OrderId, oi.ProductId });

            modelBuilder.Entity<ProductEntity>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<OrderItemEntity>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId);
        }


    }
}
