using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using back_end.Models;

namespace back_end.Data.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasData
        (
            new Product { Id = 1, Name = "Áo sơ mi trắng", Price = 250000, Description = "Đơn giản là đẹp", ImagePath = "a.png", CategoryId = 1 },
            new Product { Id = 2, Name = "Áo croptop đen", Price = 200000, Description = "Cực kì cá tính", ImagePath = "b.png", CategoryId = 2 }
        );
    }
}
