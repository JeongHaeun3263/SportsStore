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
        void SaveProduct(Product product); // product comes from model, abstract methods, we need concreate method in EFProductRepository

        Product DeleteProduct(int productID);
    }
}
