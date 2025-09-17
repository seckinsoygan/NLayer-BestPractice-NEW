using Repositories.Interfaces;
using Repositories.Products;

namespace Services.Products;

public class ProductService(IGenericRepository<Product> productRepository) : IProductService
{
}
