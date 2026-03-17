using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Order.Application.Features.Orders.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.DiscountRate).GreaterThan(0).WithMessage("Discount rate must be greater than 0.")
                .LessThanOrEqualTo(100).WithMessage("Discount rate must be less than or equal to 100.");

            RuleFor(x => x.Address).NotNull().WithMessage("Address is required.").SetValidator(new CreateAddressDtoValidator());

            RuleFor(x => x.Payment).NotNull().WithMessage("Payment is required.").SetValidator(new CreatePaymentDtoValidator());

            RuleFor(x => x.Items).NotEmpty().WithMessage("At least one order item is required.")
                .ForEach(item => item.SetValidator(new CreateOrderItemDtoValidator()));
        }
    }
    public class CreateAddressDtoValidator : AbstractValidator<AddressDto>
    {
        public CreateAddressDtoValidator()
        {
            RuleFor(x => x.Province).NotEmpty().WithMessage("Province is required.");
            RuleFor(x => x.District).NotEmpty().WithMessage("District is required.");
            RuleFor(x => x.Street).NotEmpty().WithMessage("Street is required.");
            RuleFor(x => x.ZipCode).NotEmpty().WithMessage("ZipCode is required.");
            RuleFor(x => x.Line).NotEmpty().WithMessage("Line is required.");
        }
    }
    public class CreateOrderItemDtoValidator : AbstractValidator<OrderItemDto>
    {
        public CreateOrderItemDtoValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId is required.");
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("ProductName is required.");
            RuleFor(x => x.UnitPrice).GreaterThan(0).WithMessage("UnitPrice must be greater than 0.");
        }
    }
    public class CreatePaymentDtoValidator : AbstractValidator<PaymentDto>
    {
        public CreatePaymentDtoValidator()
        {
            RuleFor(x => x.CardNumber).NotEmpty().WithMessage("CardNumber is required.");
            RuleFor(x => x.CardHolderName).NotEmpty().WithMessage("CardHolderName is required.");
            RuleFor(x => x.Expiration).NotEmpty().WithMessage("Expiration is required.");
            RuleFor(x => x.Cvc).NotEmpty().WithMessage("Cvc is required.");
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be greater than 0.");
        }
    }

    
}
