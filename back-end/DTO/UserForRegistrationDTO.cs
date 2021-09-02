using System.ComponentModel.DataAnnotations;

namespace back_end.DTO
{
	public class UserForRegistrationDTO
	{
		[Required]
		public string Name { get; set; }

		[Required]
		public string UserName { get; set; }

		[Required]
		public string Password { get; set; }

		[Required]
		public string Address { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
		public string PhoneNumber { get; set; }

		[Required]
		public string Role { get; set; }
	}
}