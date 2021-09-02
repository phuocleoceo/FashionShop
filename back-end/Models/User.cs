using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace back_end.Models
{
	public class User : IdentityUser
	{
		public string Name { get; set; }

		public string Address { get; set; }

		[NotMapped]
		public string Role { get; set; }

		public ICollection<OrderHeader> OrderHeaders { get; set; }

		public ICollection<ShoppingCart> ShoppingCarts { get; set; }

		public User()
		{
			OrderHeaders = new HashSet<OrderHeader>();
			ShoppingCarts = new HashSet<ShoppingCart>();
		}
	}
}