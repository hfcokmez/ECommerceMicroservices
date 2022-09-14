using FluentValidation;
using FreeCourse.Web.Models.ViewModels.Discounts;

namespace FreeCourse.Web.Validators
{
    public class DiscountApplyInputValidator : AbstractValidator<DiscountApplyInput>
    {
        public DiscountApplyInputValidator()
        {
            RuleFor(x => x.Code).NotEmpty()
                .WithMessage("Discount code field cannot be empty");
        }
    }
}