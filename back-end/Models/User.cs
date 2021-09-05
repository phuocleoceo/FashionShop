using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace back_end.Models
{
	public class User : IdentityUser
	{
		public string Name { get; set; }

		public string Address { get; set; }

		public string RefreshToken { get; set; }

		public DateTime RefreshTokenExpiryTime { get; set; }

		public ICollection<OrderHeader> OrderHeaders { get; set; }

		public ICollection<ShoppingCart> ShoppingCarts { get; set; }

		public User()
		{
			OrderHeaders = new HashSet<OrderHeader>();
			ShoppingCarts = new HashSet<ShoppingCart>();
		}
	}
}