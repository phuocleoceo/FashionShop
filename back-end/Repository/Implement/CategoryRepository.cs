using back_end.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using back_end.Models;
using back_end.Data;

namespace back_end.Repository.Implement;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly FSContext _db;
    public CategoryRepository(FSContext db) : base(db)
    {
        _db = db;
    }

    public async Task<bool> IsExists(int id)
    {
        return await _db.Categories.AnyAsync(c => c.Id == id);
    }
}
