using System.Threading.Tasks;
using back_end.Data;
using back_end.Models;
using back_end.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace back_end.Repository.Implement
{
	public class UserRepository : Repository<User>, IUserRepository
	{
		private readonly FSContext _db;
		public UserRepository(FSContext db) : base(db)
		{
			_db = db;
		}

		public async Task<bool> IsExists(string id)
		{
			return await _db.Users.AnyAsync(c => c.Id == id);
		}
	}
}