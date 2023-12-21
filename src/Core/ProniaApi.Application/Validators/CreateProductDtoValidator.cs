using FluentValidation;
using ProniaApi.Application.DTOs.Product;

namespace ProniaApi.Application.Validators
{
	internal class CreateProductDtoValidator:AbstractValidator<CreateProductDto>
	{
		public CreateProductDtoValidator()
		{
			RuleFor(p => p.Name).NotEmpty().WithMessage("Can not be empty").MaximumLength(50).MinimumLength(2);
			RuleFor(p => p.SKU).NotEmpty().MaximumLength(10);
			RuleFor(p => p.Price).NotEmpty().LessThanOrEqualTo(999999.99m).GreaterThanOrEqualTo(10);
			RuleFor(p => p.Description).MaximumLength(1000);

		}
	}
}
