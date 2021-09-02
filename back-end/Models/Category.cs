using System.Collections.Generic;

namespace back_end.Models
{
	public class Category
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public ICollection<Product> Products { get; set; }

		public Category()
		{
			Products = new HashSet<Product>();
		}
	}
}