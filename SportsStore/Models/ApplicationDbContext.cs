using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// add namespace
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

// This class will allow us to connect to database 

namespace SportsStore.Models
{
    public class ApplicationDbContext : DbContext
    {
        // constructor, set up basic option for our database connection 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        // to get products dataset from the database
        public DbSet<Product> Products { get; set; }

        // step1 
        public DbSet<Order> Orders { get; set; }

        // step2 
        /*
         * In cmd
         * dotnet ef migrations add Orders
         * migration script will be regenerated
         * ch 10-9
         */
    }
}
