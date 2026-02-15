using FluentValidation;
using OrderService.Application.Commands;

namespace OrderService.Application.Validators
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator() { 
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("CustomerId is required.");
            RuleFor(x => x.RestaurantId).NotEmpty().WithMessage("RestaurantId is required.");
            RuleFor(x => x.Items).NotEmpty().WithMessage("At least one order item is required.");
            RuleFor(x => x.Items).Must(items => items != null && items.Any())
             .WithMessage("An order must contain at least one item.");

            RuleForEach(x => x.Items).ChildRules(item => {
                item.RuleFor(i => i.Quantity).GreaterThan(0).WithMessage("Quantity must be at least 1.");
                item.RuleFor(i => i.Price).GreaterThan(0).WithMessage("Price must be positive.");
            });

        }
    }
}
