using back_end.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using back_end.Models;
using back_end.Data;

namespace back_end.Repository.Implement;

public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
{
    private readonly FSContext _db;
    public OrderDetailRepository(FSContext db) : base(db)
    {
        _db = db;
    }

    public async Task<bool> IsExists(int id)
    {
        return await _db.OrderDetails.AnyAsync(c => c.Id == id);
    }
}
