using FluentValidation;

namespace Course.Basket.API.Feature.Basket.AddBasketItem
{
    public class AddBasketItemCommandValidator:AbstractValidator<AddBasketItemCommand>
    {
        public AddBasketItemCommandValidator()
        {
            RuleFor(x => x.CourseId).NotEmpty().WithMessage("CourseId not empty");
            RuleFor(x => x.CourseName).NotEmpty().WithMessage("CourseName not empty");
            RuleFor(x => x.CoursePrice).GreaterThan(0).WithMessage("CoursePrice must be greater than 0");
        }
    }
}
