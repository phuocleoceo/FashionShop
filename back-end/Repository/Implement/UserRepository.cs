using back_end.Data;
using back_end.Models;
using back_end.Repository.Interface;

namespace back_end.Repository.Implement
{
	public class UserRepository : Repository<User>, IUserRepository
	{
		private readonly FSContext _db;
		public UserRepository(FSContext db) : base(db)
		{
			_db = db;
		}
	}
}