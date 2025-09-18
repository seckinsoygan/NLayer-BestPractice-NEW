using Repositories.Categories;
using Services.Categories.Create;
using Services.Categories.Update;

namespace Services.Categories;
public interface ICategoryService
{
    Task<ServiceResult<List<Category>>> GetCategoriesWithProductsAsync();
    Task<ServiceResult<CategoryWithProductDto>> GetCategoryWithProductAsync(int id);
    Task<ServiceResult<List<Category>>> GetAllAsync();
    Task<ServiceResult<Category>> GetByIdAsync(int id);
    Task<ServiceResult> DeleteAsync(int id);
    Task<ServiceResult<int>> Create(CreateCategoryRequest request);
    Task<ServiceResult> UpdateAsync(UpdateCategoryRequest request);
}
