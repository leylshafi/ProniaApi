using FluentValidation;
using ProniaApi.Application.DTOs.Users;

namespace ProniaApi.Application.Validators
{
	public class RegisterDtoValidator:AbstractValidator<RegisterDto>
	{
		public RegisterDtoValidator()
		{
			RuleFor(x => x.Email)
				.NotEmpty()
				.MaximumLength(256);
			RuleFor(x => x.Password)
				.NotEmpty()
				.MinimumLength(8)
				.MaximumLength(150);
			RuleFor(x => x.UserName)
				.NotEmpty()
				.MaximumLength(50)
				.MinimumLength(4);
			RuleFor(x => x.Name)
				.NotEmpty()
				.MinimumLength(3)
				.MaximumLength(50);
			RuleFor(x => x.Surname)
				.NotEmpty()
				.MinimumLength(3)
				.MaximumLength(50);
			RuleFor(x => x)
				.Must(x => x.ConfirmPassword == x.Password);
		}
	}
}
