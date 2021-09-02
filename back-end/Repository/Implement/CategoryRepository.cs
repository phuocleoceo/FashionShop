using back_end.Data;
using back_end.Models;
using back_end.Repository.Interface;

namespace back_end.Repository.Implement
{
	public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
		private readonly FSContext _db;
		public CategoryRepository(FSContext db) : base(db)
		{
			_db = db;
		}
	}
}