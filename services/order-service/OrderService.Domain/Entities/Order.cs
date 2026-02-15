using OrderService.Domain.Common;
using OrderService.Domain.Enums;
using OrderService.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid RestaurantId { get; private set; }
        public OrderStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private readonly List<OrderItem> _items = new();
        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

        // Constructor is private to force the use of a Factory or Create method
        private Order() { }

        public static Order Create(Guid customerId, Guid restaurantId)
        {
            return new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                RestaurantId = restaurantId,
                Status = OrderStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void AddItem(Guid productId, string name, Money price, int quantity)
        {
            if (Status != OrderStatus.Pending)
                throw new DomainException("Cannot add items to an active or completed order.");

            _items.Add(new OrderItem(productId, name, price, quantity));
        }

        public void TransitionToPaid()
        {
            if (Status != OrderStatus.Pending)
                throw new DomainException("Order must be Pending to transition to Paid.");

            Status = OrderStatus.Paid;
        }

        public decimal GetTotalAmount() => _items.Sum(x => x.UnitPrice.Amount * x.Quantity);
    }
}
