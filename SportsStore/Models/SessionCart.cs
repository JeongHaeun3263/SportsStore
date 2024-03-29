﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsStore.Infrastructure;
using Newtonsoft.Json;

namespace SportsStore.Models
{
    public class SessionCart : Cart 
    {
        // use service httpContext ( in CartController)
        public static Cart GetCart(IServiceProvider services)
        {
            //access session, one cart per session 
            ISession session = services.GetRequiredService<IHttpContextAccessor>()
                .HttpContext.Session;

            // if cart is there, it use the cart. 
            SessionCart cart = session.GetJson<SessionCart>("Cart") ?? new SessionCart();
            cart.Session = session;

            return cart;
        }
        
        [JsonIgnore] // Session property will be ignored when serializing
        public ISession Session { get; set; }

        public override void AddItem(Product product, int quantity)
        {
            base.AddItem(product, quantity);
            Session.SetJson("Cart", this);
        }

        public override void RemoveLine(Product product)
        {
            base.RemoveLine(product);
            Session.SetJson("Cart", this);
        }

        public override void Clear()
        {
            base.Clear();
            Session.Remove("Cart");
        }
    }
}
