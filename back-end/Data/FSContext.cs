using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using back_end.Models;
using Microsoft.EntityFrameworkCore;

namespace back_end.Data
{
	public class FSContext : IdentityDbContext<User>
	{
		public FSContext(DbContextOptions<FSContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			foreach (var entityType in builder.Model.GetEntityTypes())
			{
				var tableName = entityType.GetTableName();
				if (tableName.StartsWith("AspNet"))
				{
					entityType.SetTableName(tableName.Substring(6));
				}
			}
		}

		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<OrderHeader> OrderHeaders { get; set; }
		public DbSet<ShoppingCart> ShoppingCarts { get; set; }
	}
}