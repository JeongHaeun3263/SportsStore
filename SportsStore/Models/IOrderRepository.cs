using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }

        // abstract version of methods
        void SaveOrder(Order order);

    }
}
