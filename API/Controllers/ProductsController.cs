﻿using Microsoft.AspNetCore.Mvc;
using Services.Products;

namespace API.Controllers;
public class ProductsController(IProductService productService) : CustomControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll() => CreateActionResult(await productService.GetAllAsync());
    [HttpGet("{pageNumber}/{pageSize}")]
    public async Task<IActionResult> GetPagedAll(int pageNumber, int pageSize) => CreateActionResult(await productService.GetPagedAllListAsync(pageNumber, pageSize));
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) => CreateActionResult(await productService.GetByIdAsync(id));
    [HttpPost]
    public async Task<IActionResult> Create(CreateProductRequest request) => CreateActionResult(await productService.CreateAsync(request));
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateProductRequest request) => CreateActionResult(await productService.UpdateAsync(id, request));
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) => CreateActionResult(await productService.DeleteAsync(id));
}
