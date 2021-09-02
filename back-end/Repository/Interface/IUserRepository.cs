using System.Threading.Tasks;
using back_end.Models;

namespace back_end.Repository.Interface
{
	public interface IUserRepository : IRepository<User>
	{
		Task<bool> IsExists(string id);
	}
}