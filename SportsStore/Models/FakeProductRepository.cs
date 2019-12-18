using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    // hard cording 
    public class FakeProductRepository /*: IProductRepository */
    {
        // as 'IQueryable<Product>' is read only properties, we are using => 
        public IQueryable<Product> Products => new List<Product>
        {
            new Product { Name = "Football", Price = 25 },
            new Product { Name = "Surf board", Price = 179 },
            new Product { Name = "Runnign shoes", Price = 95 }
        }.AsQueryable<Product>();
    }
}
