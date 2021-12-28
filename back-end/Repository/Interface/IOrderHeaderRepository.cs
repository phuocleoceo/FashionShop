using System.Threading.Tasks;
using back_end.Models;

namespace back_end.Repository.Interface;

public interface IOrderHeaderRepository : IRepository<OrderHeader>
{
    Task<bool> IsExists(int id);
}
