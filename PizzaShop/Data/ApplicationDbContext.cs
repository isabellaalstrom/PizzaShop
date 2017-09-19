using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Entities;

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
            builder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            builder.Entity<Dish>()
                .HasOne(o => o.DishType)
                .WithMany(u => u.Dishes)
                .HasForeignKey(o => o.DishTypeId);            

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

            builder.Entity<CartItem>()
                .HasOne(di => di.Order)
                .WithMany(d => d.OrderCartItems)
                .HasForeignKey(di => di.OrderId);

            builder.Entity<CartItem>()
                .HasOne(di => di.Dish)
                .WithMany(d => d.CartItems)
                .HasForeignKey(di => di.DishId);

            builder.Entity<CartItemIngredient>()
                .HasOne(di => di.CartItem)
                .WithMany(d => d.CartItemIngredients)
                .HasForeignKey(di => di.CartItemId);

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<DishIngredient> DishIngredients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<CartItemIngredient> CartItemIngredients { get; set; }
        public DbSet<DishType> DishTypes { get; set; }
        //public DbSet<Cart> Carts { get; set; }
        public DbSet<Payment> Payment { get; set; }

    }
}
