

using FluentValidation;
using RestAPI.Example.Application.Models;

namespace RestAPI.Example.Application.Validator
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.Email).EmailAddress();

            RuleFor(u => u.Password).NotEmpty().MinimumLength(5);

            RuleFor(u => u.Department).NotEmpty();
        }
    }
}
