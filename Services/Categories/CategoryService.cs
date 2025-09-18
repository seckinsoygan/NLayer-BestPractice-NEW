
using Repositories;
using Repositories.Categories;
using Services.Categories.Create;

namespace Services.Categories;
public class CategoryService(ICategoryRepository repository, IUnitOfWork unitOfWork) : ICategoryService
{
    public async Task<ServiceResult<int>> Create(CreateCategoryRequest request)
    {
        var newCategory = new Category { Name = request.Name };

        await repository.AddAsync(newCategory);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult<int>.Success(1);
    }
}
