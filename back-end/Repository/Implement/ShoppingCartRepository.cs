using back_end.Data;
using back_end.Models;
using back_end.Repository.Interface;

namespace back_end.Repository.Implement
{
	public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
	{
		private readonly FSContext _db;
		public ShoppingCartRepository(FSContext db) : base(db)
		{
			_db = db;
		}
	}
}