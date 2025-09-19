using FluentValidation;
using Repositories.Products;

namespace Services.Products.Create;
public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    private readonly IProductRepository productRepository;
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.Name).NotNull().WithMessage("Ürün ismi gereklidir.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Ürün fiyatı 0'dan büyük olmalıdır.");
        RuleFor(x => x.Stock).GreaterThanOrEqualTo(0).WithMessage("Ürün stoğu 0'dan küçük olamaz.");
        RuleFor(x => x.CategoryId).GreaterThan(0).WithMessage("Ürün Kategori Değeri 0 'dan büyük olmalıdır.");
    }
}
