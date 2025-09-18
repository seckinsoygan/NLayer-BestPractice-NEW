
using Microsoft.EntityFrameworkCore;

namespace Repositories.Categories;
public class CategoryRepository(AppDbContext context) : GenericRepository<Category>(context), ICategoryRepository
{
    public IQueryable<Category> GetCategoriesWithProductsAsync() => Context.Categories.Include(c => c.Products).AsQueryable();

    public async Task<Category> GetCategoryWithProductAsync(int id) => await Context.Categories.Include(c => c.Products).FirstOrDefaultAsync(x => x.Id == id);
}
