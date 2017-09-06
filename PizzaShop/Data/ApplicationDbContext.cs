using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Entities;
using PizzaShop.Models;

namespace PizzaShop.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Cart>()
                .HasKey(cart => cart.CartId);

            //Säger explicit vilka ids som ska användas som primary key i dishingredient
            builder.Entity<DishIngredient>()
                .HasKey(di => new { di.DishId, di.IngredientId });
            builder.Entity<DishIngredient>()
                .HasOne(di => di.Dish)
                .WithMany(d => d.DishIngredients)
                .HasForeignKey(di => di.DishId);
            builder.Entity<DishIngredient>()
                .HasOne(di => di.Ingredient)
                .WithMany(d => d.DishIngredients)
                .HasForeignKey(di => di.IngredientId);

            //för en till många-relation räcker detta
            builder.Entity<Order>() //den som har många av det andra
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            builder.Entity<Dish>() //den som har många av det andra
                .HasOne(o => o.DishType)
                .WithMany(u => u.Dishes)
                .HasForeignKey(o => o.DishTypeId);

            builder.Entity<OrderDish>()
                .HasKey(od => new {od.OrderId, od.DishId});
            builder.Entity<OrderDish>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDishes)
                .HasForeignKey(od => od.OrderId);
            builder.Entity<OrderDish>()
                .HasOne(od => od.Dish)
                .WithMany(d => d.OrderDishes)
                .HasForeignKey(od => od.DishId);

            //builder.Entity<CartItem>()
            //    .HasOne(di => di.Cart)
            //    .WithMany(d => d.Items)
            //    .HasForeignKey(di => di.CartId);

            builder.Entity<CartItemIngredient>()
                .HasOne(di => di.CartItem)
                .WithMany(d => d.CartItemIngredients)
                .HasForeignKey(di => di.CartItemId);

            //builder.Entity<CartItemIngredient>()
            //    .HasOne(a => a.Ingredient)
            //    .WithMany(b => b.CartItemIngredients)
            //    .HasForeignKey(o => o.IngredientId);

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<DishIngredient> DishIngredients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDish> OrderDishes { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<CartItemIngredient> CartItemIngredients { get; set; }
        public DbSet<DishType> DishTypes { get; set; }
        public DbSet<Cart> Carts { get; set; }

    }
}
