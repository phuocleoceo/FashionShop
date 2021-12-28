using back_end.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using back_end.Models;
using back_end.Data;

namespace back_end.Repository.Implement;

public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
{
    private readonly FSContext _db;
    public ShoppingCartRepository(FSContext db) : base(db)
    {
        _db = db;
    }

    public async Task<bool> IsExists(int id)
    {
        return await _db.ShoppingCarts.AnyAsync(c => c.Id == id);
    }
}
