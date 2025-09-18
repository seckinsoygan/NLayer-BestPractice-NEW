using Services.Categories.Create;

namespace Services.Categories;
public interface ICategoryService
{
    Task<ServiceResult<int>> Create(CreateCategoryRequest request);
}
