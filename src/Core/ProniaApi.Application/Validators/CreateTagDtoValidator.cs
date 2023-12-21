using FluentValidation;
using ProniaApi.Domain.Entities;

namespace ProniaApi.Application.Validators
{
	internal class CreateTagDtoValidator:AbstractValidator<Tag>
	{
		public CreateTagDtoValidator()
		{
			RuleFor(t=>t.Name).NotEmpty().MaximumLength(50);
		}
	}
}
