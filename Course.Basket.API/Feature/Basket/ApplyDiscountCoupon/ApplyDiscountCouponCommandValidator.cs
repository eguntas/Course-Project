using FluentValidation;

namespace Course.Basket.API.Feature.Basket.ApplyDiscountCoupon
{
    public class ApplyDiscountCouponCommandValidator:AbstractValidator<ApplyDiscountCouponCommand>
    {
        public ApplyDiscountCouponCommandValidator()
        {
            RuleFor(x => x.Coupon).NotEmpty().WithMessage("Coupon code cannot be empty.");
            RuleFor(x => x.Rate).GreaterThan(0).WithMessage("Discount rate must be greater than zero.");
        }
    }
}
