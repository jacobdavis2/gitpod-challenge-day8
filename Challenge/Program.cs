using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Challenge
{
    public class Program
    {
        static async Task SeedDatabaseAsync(ChallengeContext context)
        {
            // Customers
            await context.Customers.AddAsync(new Customer
            {
                Id = 1,
                Name = "Billy Bob"
            });
            await context.Customers.AddAsync(new Customer
            {
                Id = 2,
                Name = "Mary Sue"
            });
            await context.Customers.AddAsync(new Customer
            {
                Id = 3,
                Name = "Helen MacMillan"
            });
            await context.Customers.AddAsync(new Customer
            {
                Id = 4,
                Name = "Smalls"
            });
            await context.Customers.AddAsync(new Customer
            {
                Id = 5,
                Name = "Marcus"
            });
            await context.Customers.AddAsync(new Customer
            {
                Id = 6,
                Name = "Enrietta"
            });

            // Products
            await context.Products.AddAsync(new Product
            {
                Id = 1,
                Name = "Milk",
                Price = 3.99
            });
            await context.Products.AddAsync(new Product
            {
                Id = 2,
                Name = "Eggs",
                Price = 5.49
            });
            await context.Products.AddAsync(new Product
            {
                Id = 3,
                Name = "Sugar",
                Price = 2.99
            });
            await context.Products.AddAsync(new Product
            {
                Id = 4,
                Name = "Avocados",
                Price = 17.99
            });
            await context.Products.AddAsync(new Product
            {
                Id = 5,
                Name = "Peas",
                Price = .38
            });
            await context.Products.AddAsync(new Product
            {
                Id = 6,
                Name = "Eye of Newt",
                Price = 6.66
            });

            // Orders
            await context.Orders.AddAsync(new Order
            {
                Id = 1,
                CustomerId = 3
            });
            await context.Orders.AddAsync(new Order
            {
                Id = 2,
                CustomerId = 5
            });

            // OrderItems
            await context.OrderItems.AddAsync(new OrderItem 
            {
                OrderId = 1,
                ProductId = 3,
                Quantity = 2
            });
            await context.OrderItems.AddAsync(new OrderItem 
            {
                OrderId = 1,
                ProductId = 1,
                Quantity = 20
            });
            await context.OrderItems.AddAsync(new OrderItem 
            {
                OrderId = 2,
                ProductId = 5,
                Quantity = 5
            });

            await context.SaveChangesAsync();
        }
        
        public static async Task Main(string[] args)
        {
            ChallengeContext _context;

            using (DbConnection _connection = new SqliteConnection("Filename=:memory:"))
            {
                _connection.Open();

                ConnectionManager.Connection = _connection;

                _context = new ChallengeContext();
                RelationalDatabaseCreator databaseCreator = 
                    (RelationalDatabaseCreator) _context.Database.GetService<IDatabaseCreator>();
                databaseCreator.CreateTables();

                await SeedDatabaseAsync(_context);        

                CreateHostBuilder(args).Build().Run();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
