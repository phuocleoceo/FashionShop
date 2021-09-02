using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end.Models
{
	public class OrderHeader
	{
		public int Id { get; set; }

		public int UserId { get; set; }
		[ForeignKey(nameof(UserId))]
		public User User { get; set; }

		public double OrderTotal { get; set; }

		public DateTime OrderDate { get; set; }

		public DateTime ShippingDate { get; set; }

		public string TrackingNumber { get; set; }

		public string Shipper { get; set; }

		public string PaymentStatus { get; set; }

		public string OrderStatus { get; set; }

		public DateTime PaymentDate { get; set; }

		public DateTime PaymentDueDate { get; set; }

		public string TransactionId { get; set; }

		public ICollection<OrderDetail> OrderDetails { get; set; }

		public OrderHeader()
		{
			OrderDetails = new HashSet<OrderDetail>();
		}
	}
}