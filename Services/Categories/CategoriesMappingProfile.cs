using Mapster;
using Repositories.Categories;
using Services.Categories.Create;
using Services.Categories.Update;

namespace Services.Categories;
public class CategoriesMappingProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CategoryDto, Category>()
        .IgnoreNullValues(true);
        config.NewConfig<Category, CategoryWithProductDto>()
        .IgnoreNullValues(true);
        config.NewConfig<CreateCategoryRequest, Category>()
        .IgnoreNullValues(true);
        config.NewConfig<UpdateCategoryRequest, Category>()
                .IgnoreNullValues(true);
    }
}
