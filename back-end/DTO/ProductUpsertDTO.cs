namespace back_end.DTO
{
	public class ProductUpsertDTO
	{
		public string Name { get; set; }

		public double Price { get; set; }

		public string ImagePath { get; set; }

		public int CategoryId { get; set; }
	}
}