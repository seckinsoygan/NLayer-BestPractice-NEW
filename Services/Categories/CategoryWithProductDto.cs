using Services.Products;

namespace Services.Categories;
public record CategoryWithProductDto(int Id, string Name, List<ProductDto> Products);
