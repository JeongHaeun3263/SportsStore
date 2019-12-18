using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
// Haeun Jeong

namespace SportsStore
{
    public class Startup
    {
        // public property 
        public IConfiguration Configuration { get; }

        // constructor
        public Startup(IConfiguration configuration) => Configuration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // connecting database to data string 
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration["Data:SportsStoreProducts:ConnectionString"]));

            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration["Data:SportsStoreIdentity:ConnectionString"]));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders(); // user can change password etc...

            // whenever the controller need data in Iproductprpository, use fakeproductrepository (just provide initial data) ??
            services.AddTransient<IProductRepository, EFProductRepository>();

            // whenever I need a Cart use get cart to return a session cart 
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            // whenever I need to access to HttpAccess
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IOrderRepository, EFOrderRepository>();

            services.AddMvc();

            // setting up memory in the server, we need to add session
            services.AddMemoryCache();
            services.AddSession();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages(); // friendly pages/message to us
            app.UseStaticFiles(); // to recongnize bootstrap files under wwwroot folder
            app.UseSession(); // to store infomation of cart 
            app.UseAuthentication(); 

            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: null,
                    template: "{category}/Page{productPage:int}",
                    defaults: new { Controller = "Product", action = "List" });

                routes.MapRoute(
                     name: null,
                     template: "Page{productPage:int}",
                     defaults: new { Controller = "Product", action = "List", productPage = 1 });

                routes.MapRoute(
                    name: null,
                    template: "{category}",
                    defaults: new { Controller = "Product", action = "List", productPage = 1 });

                // default 
                routes.MapRoute(
                name: null,
                template: "",
                defaults: new { Controller = "Product", action = "List", productPage = 1 });

                routes.MapRoute(name: null, template: "{controller}/{action}/{id?}");

            });
            SeedData.EnsurePopulated(app);
            IdentitySeedData.EnsurePopulated(app);
        }
    }
}
