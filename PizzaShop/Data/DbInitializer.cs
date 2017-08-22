using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PizzaShop.Models;

namespace PizzaShop.Data
{
    public class DbInitializer
    {
        public static void Initializer(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var regularUser = new ApplicationUser
            {
                UserName = "student@test.se",
                Email = "student@test.se"
            };
            var regularUserResult = userManager.CreateAsync(regularUser, "Pa$$w0rd").Result;

            var adminRole = new IdentityRole { Name = "Admin" };
            var roleResult = roleManager.CreateAsync(adminRole).Result;

            var adminUser = new ApplicationUser
            {
                UserName = "admin@test.se",
                Email = "admin@test.se"
            };
            var adminUserResult = userManager.CreateAsync(adminUser, "Pa$$w0rd").Result;


            if (!context.Dishes.Any())
            {
                var cheese = new Ingredient { Name = "Cheese" };
                var ham = new Ingredient { Name = "Ham" };
                var tomato = new Ingredient { Name = "Tomato" };
                var capricciosa = new Dish { Name = "Capricciosa", Price = 79 };
                var margaritha = new Dish { Name = "Margaritha", Price = 69 };
                var hawaii = new Dish { Name = "Hawaii", Price = 79 };
                var capricciosaCheese = new DishIngredient { Dish = capricciosa, Ingredient = cheese };
                var capricciosaHam = new DishIngredient { Dish = capricciosa, Ingredient = ham };
                var capricciosaTomato = new DishIngredient { Dish = capricciosa, Ingredient = tomato };
                capricciosa.DishIngredients = new List<DishIngredient>();
                capricciosa.DishIngredients.Add(capricciosaTomato);
                capricciosa.DishIngredients.Add(capricciosaHam);
                capricciosa.DishIngredients.Add(capricciosaCheese);
 
                context.Ingredients.AddRange(cheese, tomato, ham);
                context.Dishes.AddRange(capricciosa, margaritha, hawaii);
                context.DishIngredients.AddRange(capricciosaTomato, capricciosaCheese, capricciosaHam);
                context.SaveChanges();
            }
        }
    }
}
