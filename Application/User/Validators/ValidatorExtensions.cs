// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Author: Evgeniy Gusev
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Application.User.Validators
{
    using FluentValidation;

    public static class ValidatorExtensions
    {
        public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
                .NotEmpty()
                .MinimumLength(6).WithMessage("Password must be at least 6 characters").Matches("[A-Z]").
                WithMessage("Password must contain one UPPERCASE Letter").Matches("[a-z]").
                WithMessage("Password must have at least 1 lowercase character").Matches("[0-9]").WithMessage("Password must contain a number").
                Matches("[^a-zA-Z0-9]").WithMessage("Password must contain no alphanumeric");

            return options;
        }
        
    }
}
