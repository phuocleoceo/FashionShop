using System.ComponentModel.DataAnnotations.Schema;

namespace back_end.Models
{
	public class ShoppingCart
	{
		public int Id { get; set; }

		public string UserId { get; set; }
		[ForeignKey(nameof(UserId))]
		public User User { get; set; }

		public int ProductId { get; set; }
		[ForeignKey(nameof(ProductId))]
		public Product Product { get; set; }

		public int Count { get; set; } = 1;
	}
}