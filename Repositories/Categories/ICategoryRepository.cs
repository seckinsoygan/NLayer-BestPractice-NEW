using Repositories.Interfaces;

namespace Repositories.Categories;
public interface ICategoryRepository : IGenericRepository<Category>
{
    IQueryable<Category> GetCategoriesWithProductsAsync();
    Task<Category> GetCategoryWithProductAsync(int id);
}
