using System.Threading.Tasks;
using back_end.Models;

namespace back_end.Repository.Interface
{
	public interface ICategoryRepository : IRepository<Category>
	{
		Task<bool> IsExists(int id);
	}
}