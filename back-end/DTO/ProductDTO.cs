namespace back_end.DTO
{
	public class ProductDTO
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public double Price { get; set; }

		public string ImagePath { get; set; }

		public int CategoryId { get; set; }
		public string Category { get; set; }
	}
}