using Repositories;
using Repositories.Products;

namespace Services.Products;

public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork) : IProductService
{
    public async Task<ServiceResult<CreateProductResponse>> CreateAsync(string name, decimal price, int stock)
    {
        var product = new Product
        {
            Name = name,
            Price = price,
            Stock = stock
        };
        await productRepository.AddAsync(product);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult<CreateProductResponse>.Success(new CreateProductResponse(product.Id));
    }

    public async Task<ServiceResult<ProductDto?>> GetByIdAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);

        if (product == null)
        {
            ServiceResult<ProductDto?>.Fail("Product not found", System.Net.HttpStatusCode.NotFound);
        }

        var productAsDto = new ProductDto(product.Id, product.Name, product.Price, product.Stock);

        return ServiceResult<ProductDto?>.Success(productAsDto!);
    }

    public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count)
    {
        var products = await productRepository.GetTopPriceProductsAsync(count);
        var productsAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

        return new ServiceResult<List<ProductDto>>
        {
            Data = productsAsDto
        };
    }

    public async Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product is null)
        {
            ServiceResult<ProductDto?>.Fail("Product not found", System.Net.HttpStatusCode.NotFound);
        }
        product.Name = request.Name;
        product.Price = request.price;
        product.Stock = request.stock;

        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success();
    }
}
