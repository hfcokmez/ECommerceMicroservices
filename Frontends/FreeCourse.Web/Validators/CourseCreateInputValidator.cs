using FluentValidation;
using FreeCourse.Web.Models.Inputs.Courses;

namespace FreeCourse.Web.Validators
{
    public class CourseCreateInputValidator : AbstractValidator<CourseCreateInput>
    {
        public CourseCreateInputValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name field cannot be empty");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description field cannot be empty");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price field cannot be empty").ScalePrecision(2, 6)
                .WithMessage("No suitable input format");
            RuleFor(x => x.Feature.Duration).InclusiveBetween(1, int.MaxValue)
                .WithMessage("Duration field cannot be empty");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("You have to choose a category");
        }
    }
}