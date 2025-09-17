namespace Services.Products;
public record UpdateProductRequest(int id, string Name, decimal price, int stock);
