using Microsoft.AspNetCore.Mvc;
using Services.Products;

namespace API.Controllers;
public class ProductsController(IProductService productService) : CustomControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllProducts() => CreateActionResult(await productService.GetAllAsync());
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) => CreateActionResult(await productService.GetByIdAsync(id));
    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductRequest request) => CreateActionResult(await productService.CreateAsync(request));
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, UpdateProductRequest request) => CreateActionResult(await productService.UpdateAsync(id, request));
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id) => CreateActionResult(await productService.DeleteAsync(id));
}
