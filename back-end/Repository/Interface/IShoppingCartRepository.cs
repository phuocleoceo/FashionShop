using System.Threading.Tasks;
using back_end.Models;

namespace back_end.Repository.Interface;

public interface IShoppingCartRepository : IRepository<ShoppingCart>
{
    Task<bool> IsExists(int id);
}
