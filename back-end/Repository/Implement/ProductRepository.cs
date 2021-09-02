using System.Threading.Tasks;
using back_end.Data;
using back_end.Models;
using back_end.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace back_end.Repository.Implement
{
	public class ProductRepository : Repository<Product>, IProductRepository
	{
		private readonly FSContext _db;
		public ProductRepository(FSContext db) : base(db)
		{
			_db = db;
		}

		public async Task<bool> IsExists(int id)
		{
			return await _db.Products.AnyAsync(c => c.Id == id);
		}
	}
}