using back_end.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace back_end.Data.Configuration
{
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.HasData
			(
				new Product { Id = 1, Name = "Áo sơ mi trắng", Price = 250000, ImagePath = "a.png", CategoryId = 1 },
				new Product { Id = 2, Name = "Áo croptop đen", Price = 200000, ImagePath = "b.png", CategoryId = 2 }
			);
		}
	}
}