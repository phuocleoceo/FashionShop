using back_end.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace back_end.Data.Configuration
{
	public class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.HasData
			(
				new Category { Id = 1, Name = "Áo nam" },
				new Category { Id = 2, Name = "Áo nữ" }
			);
		}
	}
}