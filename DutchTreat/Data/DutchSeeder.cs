using Dutch.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dutch.Data
{
    public class DutchSeeder
    {
        private readonly DutchContext _ctx;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<StoreUser> _userManager;
        public DutchSeeder(DutchContext ctx, IWebHostEnvironment env, UserManager<StoreUser> userManager)
        {
            _ctx = ctx;
            _env = env;
            _userManager = userManager;
        }
        public void SeedAsync()
        {
            _ctx.Database.EnsureCreated();
            StoreUser user =  _userManager.FindByEmailAsync("viktor@dutchtreat.com").Result;
            if(user == null)
            {
                user = new StoreUser()
                {
                    FirstName = "Viktor",
                    LastName = "Zafirovski",
                    Email = "viktor@dutchtreat.com",
                    UserName = "viktor@dutchtreat.com"
                };
                var result =  _userManager.CreateAsync(user, "Vik_goa02!").Result;
                if(result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create new user in seeder");
                }
            }
            if (!_ctx.Products.Any())
            {
                var filePath = Path.Combine(_env.ContentRootPath,"Data/art.json");
                var json = File.ReadAllText(filePath);
                var products = JsonSerializer.Deserialize<IEnumerable<Product>>(json);
                _ctx.Products.AddRange(products);
                var order = new Order
                {
                    OrderDate = DateTime.Today,
                    OrderNumber = "10000",
                    Items = new List<OrderItem>()
                    {
                        new OrderItem
                        {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    },
                    User = user
                };
                _ctx.Orders.Add(order);
                _ctx.SaveChanges();
            }
        }
    }
}
