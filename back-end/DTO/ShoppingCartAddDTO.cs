using System.ComponentModel.DataAnnotations;

namespace back_end.DTO
{
	public class ShoppingCartAddDTO
	{
		[Required]
		public string UserId { get; set; }

		[Required]
		public int ProductId { get; set; }

		[Required]
		[Range(1,999999)]
		public int Count { get; set; } = 1;
	}
}