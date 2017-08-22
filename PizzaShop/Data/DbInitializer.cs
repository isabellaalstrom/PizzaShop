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
                var tomatoSauce = new Ingredient { Name = "Tomato" };
                var mushroom = new Ingredient { Name = "Mushroom" };
                var shrimp = new Ingredient { Name = "Shrimp" };
                var tuna = new Ingredient { Name = "Tuna" };
                var pineapple = new Ingredient { Name = "Pineapple" };
                var curry = new Ingredient { Name = "Curry" };
                var bacon = new Ingredient { Name = "Bacon" };
                var onion = new Ingredient { Name = "Onion" };


                var margherita = new Dish { Name = "Margherita", Price = 89 };
                var fungi = new Dish { Name = "Fungi", Price = 91 };
                var capricciosa = new Dish { Name = "Capricciosa", Price = 99 };
                var hawaii = new Dish { Name = "Hawaii", Price = 99 };

                var margheritaCheese = new DishIngredient { Dish = margherita, Ingredient = cheese };
                var margheritaTomatoSouce = new DishIngredient { Dish = margherita, Ingredient = tomatoSauce };

                var capricciosaCheese = new DishIngredient { Dish = capricciosa, Ingredient = cheese };
                var capricciosaHam = new DishIngredient { Dish = capricciosa, Ingredient = ham };
                var capricciosaTomatoSauce = new DishIngredient { Dish = capricciosa, Ingredient = tomatoSauce };

                var fungiTomatoSauce = new DishIngredient {Dish = fungi, Ingredient = tomatoSauce};
                var fungiCheese = new DishIngredient { Dish = fungi, Ingredient = cheese };
                var fungiMushroom = new DishIngredient { Dish = fungi, Ingredient = mushroom };


                capricciosa.DishIngredients = new List<DishIngredient>
                {
                    capricciosaTomatoSauce,
                    capricciosaHam,
                    capricciosaCheese,
                    margheritaCheese,
                    margheritaTomatoSouce,
                    fungiMushroom,
                    fungiCheese,
                    fungiTomatoSauce
                };

                context.Ingredients.AddRange(cheese, tomatoSauce, ham, mushroom, bacon, curry, onion, pineapple, shrimp, tuna);
                context.Dishes.AddRange(capricciosa, margherita, hawaii, fungi);
                context.DishIngredients.AddRange(capricciosaTomatoSauce, capricciosaCheese,
                    capricciosaHam, margheritaCheese, margheritaTomatoSouce, fungiMushroom,
                    fungiCheese, fungiTomatoSauce);
                context.SaveChanges();
            }
        }
    }
}
