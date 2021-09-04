namespace back_end.DTO
{
	public class ShoppingCartDTO
	{
		public int Id { get; set; }

		public string UserId { get; set; }

		public int ProductId { get; set; }
		public string Product { get; set; }
		public double Price { get; set; }

		public int Count { get; set; }

		public double Total => Price * Count;
	}
}