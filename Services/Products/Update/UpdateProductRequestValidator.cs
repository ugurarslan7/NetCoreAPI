using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Products.Update
{
    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Ürün ismi gereklidir.")
                .Length(3, 20).WithMessage("Ürün ismi 3 ile 20 karakter arası olmalıdır.");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Fiyat 0 dan büyük olmalıdır.");

            RuleFor(p => p.CategoryId)
             .GreaterThan(0).WithMessage("Kategori 0 dan büyük olmalıdır.");

            RuleFor(p => p.Price)
               .InclusiveBetween(1, 100).WithMessage("Stok miktarı 1 ile 100 arasında olmalıdır.");
        }
    }
}
