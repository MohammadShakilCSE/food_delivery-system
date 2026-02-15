using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Application.Commands
{
    public record CreateOrderCommand(
    Guid CustomerId,
    Guid RestaurantId,
    List<OrderItemDto> Items) : IRequest<Guid>; // Returns the new Order ID

    public record OrderItemDto(Guid ProductId, string Name, decimal Price, int Quantity);
}
