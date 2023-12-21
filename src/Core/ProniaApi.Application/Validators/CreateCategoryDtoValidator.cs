using FluentValidation;
using ProniaApi.Application.DTOs.Category;

namespace ProniaApi.Application.Validators
{
	internal class CreateCategoryDtoValidator:AbstractValidator<CreateCategoryDto>
	{
		public CreateCategoryDtoValidator()
		{
			RuleFor(x => x.Name).NotEmpty().MaximumLength(50).MinimumLength(1);
		}
	}
}
