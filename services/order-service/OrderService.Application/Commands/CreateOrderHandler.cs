using MediatR;
using OrderService.Application.Common.Interfaces;
using OrderService.Domain.Common;
using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Application.Commands
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderRepository _repository; // Interface defined in Domain

        public CreateOrderHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken ct)
        {
            // 1. Create the Aggregate Root using Domain logic
            var order = Order.Create(request.CustomerId, request.RestaurantId);

            // 2. Add items through the Domain's guarded method
            foreach (var item in request.Items)
            {
                var price = Money.Create(item.Price);
                order.AddItem(item.ProductId, item.Name, price, item.Quantity);
            }

            // 3. Persist via Repository (Infrastructure will implement this)
            await _repository.AddAsync(order);

            return order.Id;
        }
    }
}
