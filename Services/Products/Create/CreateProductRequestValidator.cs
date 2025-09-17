using FluentValidation;
using Repositories.Products;

namespace Services.Products.Create;
public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    private readonly IProductRepository productRepository;
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.Name).NotNull().WithMessage("Ürün ismi gereklidir.").Must(MustUniqueProductName).WithMessage("Ürün ismi veritabanında bulunmaktadır.");
        RuleFor(x => x.price).GreaterThan(0).WithMessage("Ürün fiyatı 0'dan büyük olmalıdır.");
        RuleFor(x => x.stock).GreaterThanOrEqualTo(0).WithMessage("Ürün stoğu 0'dan küçük olamaz.");
    }
    private bool MustUniqueProductName(string name)
    {
        return productRepository.Where(x => x.Name == name).Any();
    }
}
