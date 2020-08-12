using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Challenge
{
    public class ChallengeContext : DbContext
    {
        public async Task InitDatabaseAsync()
        {
            RelationalDatabaseCreator databaseCreator = 
                (RelationalDatabaseCreator) Database.GetService<IDatabaseCreator>();
            databaseCreator.CreateTables(); 

            // Customers
            await Customers.AddAsync(new Customer
            {
                Id = 1,
                Name = "Billy Bob"
            });
            await Customers.AddAsync(new Customer
            {
                Id = 2,
                Name = "Mary Sue"
            });
            await Customers.AddAsync(new Customer
            {
                Id = 3,
                Name = "Helen MacMillan"
            });
            await Customers.AddAsync(new Customer
            {
                Id = 4,
                Name = "Smalls"
            });
            await Customers.AddAsync(new Customer
            {
                Id = 5,
                Name = "Marcus"
            });
            await Customers.AddAsync(new Customer
            {
                Id = 6,
                Name = "Enrietta"
            });

            // Products
            await Products.AddAsync(new Product
            {
                Id = 1,
                Name = "Milk",
                Price = 3.99
            });
            await Products.AddAsync(new Product
            {
                Id = 2,
                Name = "Eggs",
                Price = 5.49
            });
            await Products.AddAsync(new Product
            {
                Id = 3,
                Name = "Sugar",
                Price = 2.99
            });
            await Products.AddAsync(new Product
            {
                Id = 4,
                Name = "Avocados",
                Price = 17.99
            });
            await Products.AddAsync(new Product
            {
                Id = 5,
                Name = "Peas",
                Price = .38
            });
            await Products.AddAsync(new Product
            {
                Id = 6,
                Name = "Eye of Newt",
                Price = 6.66
            });

            // Orders
            await Orders.AddAsync(new Order
            {
                Id = 1,
                CustomerId = 3
            });
            await Orders.AddAsync(new Order
            {
                Id = 2,
                CustomerId = 5
            });

            // OrderItems
            await OrderItems.AddAsync(new OrderItem 
            {
                OrderId = 1,
                ProductId = 3,
                Quantity = 2
            });
            await OrderItems.AddAsync(new OrderItem 
            {
                OrderId = 1,
                ProductId = 1,
                Quantity = 20
            });
            await OrderItems.AddAsync(new OrderItem 
            {
                OrderId = 2,
                ProductId = 5,
                Quantity = 5
            });

            await SaveChangesAsync();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(ConnectionManager.Connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>()
                .HasOne<Order>(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne<Product>(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId);

            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.ProductId, oi.OrderId } );

            modelBuilder.Entity<Order>()
                .HasOne<Customer>(o => o.Customer)
                .WithOne(c => c.Order)
                .HasForeignKey<Order>(o => o.CustomerId);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}