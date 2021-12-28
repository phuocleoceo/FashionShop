using back_end.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using back_end.Models;
using back_end.Data;

namespace back_end.Repository.Implement;

public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
{
    private readonly FSContext _db;
    public OrderHeaderRepository(FSContext db) : base(db)
    {
        _db = db;
    }

    public async Task<bool> IsExists(int id)
    {
        return await _db.OrderHeaders.AnyAsync(c => c.Id == id);
    }
}
