using System;
using System.Threading.Tasks;

namespace back_end.Repository.Interface
{
	public interface IUnitOfWork : IDisposable
	{
		IUserRepository Users { get; }
		ICategoryRepository Categories { get; }
		IProductRepository Products { get; }
		IShoppingCartRepository ShoppingCarts { get; }
		IOrderHeaderRepository OrderHeaders { get; }
		IOrderDetailRepository OrderDetails { get; }

		Task SaveChanges();
	}
}