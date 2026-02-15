using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Domain.Common
{
    public record Money(decimal Amount, string Currency)
    {
        public static Money Create(decimal amount, string currency = "USD")
        {
            if (amount < 0) throw new ArgumentException("Amount cannot be negative.");
            return new Money(amount, currency);
        }
    }
}
