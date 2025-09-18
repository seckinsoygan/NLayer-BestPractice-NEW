
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Categories;
using Services.Categories.Create;
using Services.Categories.Update;
using System.Net;

namespace Services.Categories;
public class CategoryService(ICategoryRepository repository, IUnitOfWork unitOfWork, IMapper mapper) : ICategoryService
{
    public async Task<ServiceResult<List<Category>>> GetCategoriesWithProductsAsync()
    {
        var categories = await repository.GetCategoriesWithProductsAsync().ToListAsync();

        return ServiceResult<List<Category>>.Success(categories);
    }
    public async Task<ServiceResult<CategoryWithProductDto>> GetCategoryWithProductAsync(int id)
    {
        var category = await repository.GetCategoryWithProductAsync(id);
        if (category is null)
        {
            return ServiceResult<CategoryWithProductDto>.Fail("Category not found.", HttpStatusCode.NotFound);
        }

        var categoryAsDto = mapper.Map<CategoryWithProductDto>(category);

        return ServiceResult<CategoryWithProductDto>.Success(categoryAsDto);
    }
    public async Task<ServiceResult<List<Category>>> GetAllAsync()
    {
        var categories = await repository.GetAll().ToListAsync();
        return ServiceResult<List<Category>>.Success(categories);
    }
    public async Task<ServiceResult<Category>> GetByIdAsync(int id)
    {
        var category = await repository.GetByIdAsync(id);
        if (category is null)
        {
            return ServiceResult<Category>.Fail("Category not found.", HttpStatusCode.NotFound);
        }
        return ServiceResult<Category>.Success(category);
    }
    public async Task<ServiceResult<int>> Create(CreateCategoryRequest request)
    {
        var anyCategory = await repository.Where(x => x.Name == request.Name).AnyAsync();

        if (anyCategory)
        {
            return ServiceResult<int>.Fail("Category with the same name already exists.", HttpStatusCode.NotFound);
        }

        var newCategory = new Category { Name = request.Name };

        await repository.AddAsync(newCategory);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult<int>.Success(1);
    }
    public async Task<ServiceResult> UpdateAsync(UpdateCategoryRequest request)
    {
        var category = await repository.GetByIdAsync(request.Id);

        if (category is null)
        {
            return ServiceResult.Fail("Category not found.", HttpStatusCode.NotFound);
        }
        var isCategoryNameExists = await repository.Where(x => x.Name == request.Name && x.Id != request.Id).AnyAsync();
        if (isCategoryNameExists)
        {
            return ServiceResult.Fail("Category with the same name already exists.", HttpStatusCode.BadRequest);
        }
        category = mapper.Map(request, category);

        await repository.AddAsync(category);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);

    }
    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var category = await repository.GetByIdAsync(id);
        if (category is null)
        {
            return ServiceResult.Fail("Category not found.", HttpStatusCode.NotFound);
        }
        repository.Delete(category);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}
