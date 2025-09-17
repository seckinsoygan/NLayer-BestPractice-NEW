namespace Services.Products;
public record CreateProductRequest(string Name, decimal price, int stock);
