using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public interface IProductRepository
    {
        // interface does not have any parameters 
        IQueryable<Product> Products { get; }
    }
}
