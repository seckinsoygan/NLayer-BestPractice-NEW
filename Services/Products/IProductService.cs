namespace Services.Products;
public interface IProductService
{
    Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count);
    Task<ServiceResult<ProductDto?>> GetByIdAsync(int id);
    Task<ServiceResult<CreateProductResponse>> CreateAsync(string name, decimal price, int stock);
    Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request);
}
