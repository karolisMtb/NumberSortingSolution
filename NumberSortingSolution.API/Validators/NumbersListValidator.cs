using FluentValidation;

namespace NumberSortingSolution.API.Validators
{
    public class NumbersListValidator : AbstractValidator<List<int>>
    {
        public NumbersListValidator()
        {
            RuleFor(numbers => numbers)
                .NotNull().WithMessage("List cannot be null.")
                .NotEmpty().WithMessage("List cannot be empty.");
        }
    }
}