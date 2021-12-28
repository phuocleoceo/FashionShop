using back_end.Repository.Interface;
using System.Threading.Tasks;
using back_end.Data;

namespace back_end.Repository.Implement;

public class UnitOfWork : IUnitOfWork
{
    private readonly FSContext _db;
    public UnitOfWork(FSContext db)
    {
        _db = db;
        Products = new ProductRepository(_db);
        Categories = new CategoryRepository(_db);
        ShoppingCarts = new ShoppingCartRepository(_db);
        OrderHeaders = new OrderHeaderRepository(_db);
        OrderDetails = new OrderDetailRepository(_db);
        Users = new UserRepository(_db);
    }

    public IUserRepository Users { get; private set; }

    public ICategoryRepository Categories { get; private set; }

    public IProductRepository Products { get; private set; }

    public IShoppingCartRepository ShoppingCarts { get; private set; }

    public IOrderHeaderRepository OrderHeaders { get; private set; }

    public IOrderDetailRepository OrderDetails { get; private set; }


    public void Dispose()
    {
        _db.Dispose();
    }

    public async Task SaveChanges()
    {
        await _db.SaveChangesAsync();
    }
}
