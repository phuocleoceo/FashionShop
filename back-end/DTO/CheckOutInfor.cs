using System.Collections.Generic;

namespace back_end.DTO
{
	public class CheckOutInfor
	{
		public IEnumerable<ShoppingCartDTO> ListCart { get; set; }

		public string Name { get; set; }

		public string PhoneNumber { get; set; }

		public string Address { get; set; }

		public double OrderTotal { get; set; }
	}
}