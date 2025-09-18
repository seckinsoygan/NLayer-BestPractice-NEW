using FluentValidation;

namespace Services.Products.Update;
public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(x => x.Name).NotNull().WithMessage("Ürün ismi gereklidir.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Ürün fiyatı 0'dan büyük olmalıdır.");
        RuleFor(x => x.Stock).GreaterThanOrEqualTo(0).WithMessage("Ürün stoğu 0'dan küçük olamaz.");
    }
}
