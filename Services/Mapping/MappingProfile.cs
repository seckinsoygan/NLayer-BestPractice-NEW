using Mapster;
using Repositories.Products;
using Services.Products;

namespace Services.Mapping;
public class MappingProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ProductDto, Product>()
              .IgnoreNullValues(true);
    }
}
