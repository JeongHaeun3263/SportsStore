using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        // constructor 
        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        // 'VeiwResult' is default action 
        // the products list will be sent to list view 
        public ViewResult List() => View(repository.Products);


    }
}
