using System.Threading.Tasks;
using back_end.Models;

namespace back_end.Repository.Interface
{
	public interface IProductRepository : IRepository<Product>
	{
		Task<bool> IsExists(int id);
	}
}