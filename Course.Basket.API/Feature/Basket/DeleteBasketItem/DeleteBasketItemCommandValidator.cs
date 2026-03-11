using FluentValidation;

namespace Course.Basket.API.Feature.Basket.DeleteBasketItem
{
    public class DeleteBasketItemCommandValidator:AbstractValidator<DeleteBasketItemCommand>
    {
        public DeleteBasketItemCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("CourseId is required");
        }
    }
}
