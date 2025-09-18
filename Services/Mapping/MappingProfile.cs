using Mapster;
using Repositories.Products;
using Services.Products;
using Services.Products.Create;
using Services.Products.Update;

namespace Services.Mapping;
public class MappingProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ProductDto, Product>()
                .IgnoreNullValues(true);
        config.NewConfig<CreateProductRequest, Product>()
                .IgnoreNullValues(true);
        config.NewConfig<UpdateProductRequest, Product>()
                .IgnoreNullValues(true);
    }
}
