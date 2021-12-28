using System.Threading.Tasks;
using back_end.Models;

namespace back_end.Repository.Interface;

public interface IOrderDetailRepository : IRepository<OrderDetail>
{
    Task<bool> IsExists(int id);
}
