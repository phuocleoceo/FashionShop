using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end.Models
{
	public class Product
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public double Price { get; set; }

		public string Description { get; set; }

		public string ImagePath { get; set; }

		public int CategoryId { get; set; }
		[ForeignKey(nameof(CategoryId))]
		public Category Category { get; set; }

		public ICollection<ShoppingCart> ShoppingCarts { get; set; }

		public ICollection<OrderDetail> OrderDetails { get; set; }

		public Product()
		{
			ShoppingCarts = new HashSet<ShoppingCart>();
			OrderDetails = new HashSet<OrderDetail>();
		}
	}
}