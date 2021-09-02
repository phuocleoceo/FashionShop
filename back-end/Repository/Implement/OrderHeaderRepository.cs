using back_end.Data;
using back_end.Models;
using back_end.Repository.Interface;

namespace back_end.Repository.Implement
{
	public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
	{
		private readonly FSContext _db;
		public OrderHeaderRepository(FSContext db) : base(db)
		{
			_db = db;
		}
	}
}