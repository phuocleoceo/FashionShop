using back_end.Data;
using back_end.Models;
using back_end.Repository.Interface;

namespace back_end.Repository.Implement
{
	public class ProductRepository : Repository<Product>, IProductRepository
	{
		private readonly FSContext _db;
		public ProductRepository(FSContext db) : base(db)
		{
			_db = db;
		}
	}
}