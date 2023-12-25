using FluentValidation;
using ProniaApi.Application.DTOs.Product;

namespace ProniaApi.Application.Validators
{
	public class UpdateProductDtoValidator:AbstractValidator<ProductUpdateDto>
	{
		public UpdateProductDtoValidator()
		{
			RuleFor(p => p.Name).NotEmpty().WithMessage("Can not be empty").MaximumLength(50).MinimumLength(2);
			RuleFor(p => p.SKU).NotEmpty().MaximumLength(10);
			RuleFor(p => p.Price).NotEmpty().LessThanOrEqualTo(999999.99m).GreaterThanOrEqualTo(10);
			RuleFor(p => p.Description).MaximumLength(1000);
			RuleFor(p => p.CategoryId).Must(c => c > 0);
			RuleForEach(p => p.ColorIds).Must(c => c > 0).NotNull();
			RuleFor(p => p.ColorIds).NotNull();
		}
	}
}
