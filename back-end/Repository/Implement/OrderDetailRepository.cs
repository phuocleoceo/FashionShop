using back_end.Data;
using back_end.Models;
using back_end.Repository.Interface;

namespace back_end.Repository.Implement
{
	public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
	{
		private readonly FSContext _db;
		public OrderDetailRepository(FSContext db) : base(db)
		{
			_db = db;
		}
	}
}