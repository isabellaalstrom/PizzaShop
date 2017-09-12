using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PizzaShop.Entities;
using PizzaShop.Models;

namespace PizzaShop.Data
{
    public class DbInitializer
    {
        public static void Initializer(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var studentUser = new ApplicationUser
            {
                UserName = "student@test.se",
                Email = "student@test.se",
                Orders = new List<Order>(),
                Name = "Isabella Gross",
                Address = "Mjärdgatan 8",
                Zipcode = "13343",
                City = "Saltsjöbaden",
                PhoneNumber = "0707350345"
            };
            var studentUserResult = userManager.CreateAsync(studentUser, "Pa$$w0rd").Result;
            var teacherUser = new ApplicationUser
            {
                UserName = "teacher@test.se",
                Email = "teacher@test.se",
                Name = "Isabella Gross",
                Address = "Mjärdgatan 8",
                Zipcode = "13343",
                City = "Saltsjöbaden",
                PhoneNumber = "0707350345"
            };
            var teacherUserResult = userManager.CreateAsync(teacherUser, "Pa$$w0rd").Result;

            var adminRole = new IdentityRole { Name = "Admin" };
            var roleResult = roleManager.CreateAsync(adminRole).Result;

            var adminUser = new ApplicationUser
            {
                UserName = "admin@test.se",
                Email = "admin@test.se",
                Name = "Isabella Gross Alström",
                Address = "Mjärdgatan 8",
                Zipcode = "13343",
                City = "Saltsjöbaden",
                PhoneNumber = "0707350345"
            };
            var adminUserResult = userManager.CreateAsync(adminUser, "Pa$$w0rd").Result;
            var result = userManager.AddToRoleAsync(adminUser, "Admin");

            //todo refactor
            if (!context.Dishes.Any())
            {
                var cheese = new Ingredient { IngredientName = "Cheese", Price = 5 };
                var ham = new Ingredient { IngredientName = "Ham", Price = 10 };
                var tomatoSauce = new Ingredient { IngredientName = "Tomato Sauce", Price = 5 };
                var mushroom = new Ingredient { IngredientName = "Mushroom", Price = 5 };
                var shrimp = new Ingredient { IngredientName = "Shrimp", Price = 10 };
                var tuna = new Ingredient { IngredientName = "Tuna", Price = 10 };
                var pineapple = new Ingredient { IngredientName = "Pineapple", Price = 5 };
                var curry = new Ingredient { IngredientName = "Curry", Price = 5 };
                var bacon = new Ingredient { IngredientName = "Bacon", Price = 5 };
                var banana = new Ingredient { IngredientName = "Banana", Price = 5 };
                var meat = new Ingredient { IngredientName = "Meat", Price = 15 };
                var cream = new Ingredient { IngredientName = "Cream", Price = 5 };
                var onion = new Ingredient { IngredientName = "Onion", Price = 5 };
                var lettuce = new Ingredient { IngredientName = "Lettuce", Price = 5 };
                var olives = new Ingredient { IngredientName = "Olives", Price = 5 };
                var chicken = new Ingredient { IngredientName = "Chicken", Price = 15 };
                var feta = new Ingredient { IngredientName = "Feta", Price = 5 };
                var arugula = new Ingredient { IngredientName = "Ruccola", Price = 5 };
                var parmigiano = new Ingredient { IngredientName = "Parmigiano", Price = 5 };
                var tomato = new Ingredient { IngredientName = "Tomato", Price = 5 };
                context.Ingredients.AddRange(cheese, tomatoSauce, ham, mushroom, bacon, curry, banana, pineapple, shrimp, tuna, lettuce, olives, chicken, feta, arugula, parmigiano, tomato);
                
                var pizzaDishType = new DishType { DishTypeName = "Pizza" };
                var saladDishType = new DishType { DishTypeName = "Salad" };
                var pastaDishType = new DishType { DishTypeName = "Pasta" };
                context.DishTypes.AddRange(pizzaDishType, saladDishType, pastaDishType);
                
                var margherita = new Dish { DishName = "Margherita", Price = 89, DishType = pizzaDishType };
                var margheritaCheese = new DishIngredient { Dish = margherita, Ingredient = cheese };
                var margheritaTomatoSouce = new DishIngredient { Dish = margherita, Ingredient = tomatoSauce };

                var fungi = new Dish { DishName = "Fungi", Price = 91, DishType = pizzaDishType };
                var fungiTomatoSauce = new DishIngredient { Dish = fungi, Ingredient = tomatoSauce };
                var fungiCheese = new DishIngredient { Dish = fungi, Ingredient = cheese };
                var fungiMushroom = new DishIngredient { Dish = fungi, Ingredient = mushroom };

                var capricciosa = new Dish { DishName = "Capricciosa", Price = 99, DishType = pizzaDishType };
                var capricciosaCheese = new DishIngredient { Dish = capricciosa, Ingredient = cheese };
                var capricciosaHam = new DishIngredient { Dish = capricciosa, Ingredient = ham };
                var capricciosaTomatoSauce = new DishIngredient { Dish = capricciosa, Ingredient = tomatoSauce };

                var hawaii = new Dish { DishName = "Hawaii", Price = 99, DishType = pizzaDishType };
                var hawaiiTomatoSauce = new DishIngredient { Dish = hawaii, Ingredient = tomatoSauce };
                var hawaiiCheese = new DishIngredient { Dish = hawaii, Ingredient = cheese };
                var hawaiiCurry = new DishIngredient { Dish = hawaii, Ingredient = curry };
                var hawaiiHam = new DishIngredient { Dish = hawaii, Ingredient = ham };
                var hawaiiMushroom = new DishIngredient { Dish = hawaii, Ingredient = mushroom };
                var hawaiiBanana = new DishIngredient { Dish = hawaii, Ingredient = banana };
                var hawaiiPineapple = new DishIngredient { Dish = hawaii, Ingredient = pineapple };

                var carbonara = new Dish { DishName = "Carbonara", Price = 89, DishType = pastaDishType };
                var carbonaraCheese = new DishIngredient { Dish = carbonara, Ingredient = tomatoSauce };
                var carbonaraCream = new DishIngredient { Dish = carbonara, Ingredient = cream };
                var carbonaraBacon = new DishIngredient { Dish = carbonara, Ingredient = bacon };

                var lasagne = new Dish { DishName = "Lasagne", Price = 95, DishType = pastaDishType };
                var lasagneCheese = new DishIngredient { Dish = lasagne, Ingredient = cheese };
                var lasagneCream = new DishIngredient { Dish = lasagne, Ingredient = cream };
                var lasagneMeat = new DishIngredient { Dish = lasagne, Ingredient = meat };

                var pastaConTono = new Dish { DishName = "Pasta Con Tono", Price = 92, DishType = pastaDishType };
                var pastaConTonoTomatoSauce = new DishIngredient { Dish = pastaConTono, Ingredient = tomatoSauce };
                var pastaConTonoTuna = new DishIngredient { Dish = pastaConTono, Ingredient = tuna };
                var pastaConTonoOnion = new DishIngredient { Dish = pastaConTono, Ingredient = onion };

                var greekSalad = new Dish { DishName = "Greek Salad", Price = 79, DishType = saladDishType };
                var greekSaladLettuce = new DishIngredient { Dish = greekSalad, Ingredient = lettuce };
                var greekSaladOlives = new DishIngredient { Dish = greekSalad, Ingredient = olives };
                var greekSaladChicken = new DishIngredient { Dish = greekSalad, Ingredient = chicken };
                var greekSaladFeta = new DishIngredient { Dish = greekSalad, Ingredient = feta };
                var greekSaladTomatoes = new DishIngredient { Dish = greekSalad, Ingredient = tomato };

                context.DishIngredients.AddRange(capricciosaTomatoSauce, capricciosaCheese,
                    capricciosaHam, margheritaCheese, margheritaTomatoSouce, fungiMushroom,
                    fungiCheese, fungiTomatoSauce, hawaiiPineapple, hawaiiBanana, hawaiiCheese,
                    hawaiiCurry, hawaiiHam, hawaiiMushroom, hawaiiTomatoSauce, carbonaraCheese,
                    carbonaraCream, carbonaraBacon, lasagneCheese, lasagneCream, lasagneMeat,
                    pastaConTonoTuna, pastaConTonoTomatoSauce, pastaConTonoOnion,
                    greekSaladChicken, greekSaladFeta, greekSaladLettuce, greekSaladOlives, greekSaladTomatoes);
                //capricciosa.DishIngredients = new List<DishIngredient>();
                //capricciosa.DishIngredients.Add(capricciosaTomatoSauce);
                //capricciosa.DishIngredients.Add(capricciosaHam);
                //capricciosa.DishIngredients.Add(capricciosaCheese);

                //margherita.DishIngredients = new List<DishIngredient>();
                //margherita.DishIngredients.Add(margheritaCheese);
                //margherita.DishIngredients.Add(margheritaTomatoSouce);

                //fungi.DishIngredients = new List<DishIngredient>();
                //fungi.DishIngredients.Add(fungiMushroom);
                //fungi.DishIngredients.Add(fungiCheese);
                //fungi.DishIngredients.Add(fungiTomatoSauce);

                //hawaii.DishIngredients = new List<DishIngredient>();
                //hawaii.DishIngredients.Add(hawaiiTomatoSauce);
                //hawaii.DishIngredients.Add(hawaiiCheese);
                //hawaii.DishIngredients.Add(hawaiiCurry);
                //hawaii.DishIngredients.Add(hawaiiHam);
                //hawaii.DishIngredients.Add(hawaiiMushroom);
                //hawaii.DishIngredients.Add(hawaiiBanana);
                //hawaii.DishIngredients.Add(hawaiiPineapple);

                var firstOrder = new Order();
                firstOrder.OrderDateTime = DateTime.Now;
                firstOrder.User = studentUser;
                firstOrder.OrderId = 1;

                var firstOrderMargherita = new CartItem { Dish = margherita, Order = firstOrder, IsModified = true, Price = margherita.Price };
                var cartItemMCheese = new CartItemIngredient
                {
                    CartItem = firstOrderMargherita,
                    IngredientName = "Cheese",
                    IsOriginalIngredient = true
                };
                var cartItemMTomatoSauce = new CartItemIngredient
                {
                    CartItem = firstOrderMargherita,
                    IngredientName = "Tomato Sauce",
                    IsOriginalIngredient = true
                };
                var cartItemMMushroom = new CartItemIngredient
                {
                    CartItem = firstOrderMargherita,
                    IngredientName = "Mushroom",
                    IsOriginalIngredient = false,
                    Price = 5
                };

                firstOrderMargherita.CartItemIngredients =
                    new List<CartItemIngredient> {cartItemMMushroom, cartItemMCheese, cartItemMTomatoSauce};

                var firstOrderFungi = new CartItem { Dish = fungi, Order = firstOrder, IsModified = false, Price = fungi.Price };
                var cartItemFCheese = new CartItemIngredient
                {
                    CartItem = firstOrderFungi,
                    IngredientName = "Cheese",
                    IsOriginalIngredient = true
                };
                var cartItemFTomatoSauce = new CartItemIngredient
                {
                    CartItem = firstOrderFungi,
                    IngredientName = "Tomato Sauce",
                    IsOriginalIngredient = true
                };
                var cartItemFMushroom = new CartItemIngredient
                {
                    CartItem = firstOrderFungi,
                    IngredientName = "Mushroom",
                    IsOriginalIngredient = true
                };
                firstOrderFungi.CartItemIngredients = new List<CartItemIngredient>{cartItemFMushroom, cartItemFCheese, cartItemFTomatoSauce};

                var firstOrderItems = new List<CartItem> {firstOrderMargherita, firstOrderFungi};
                firstOrder.OrderCartItems = firstOrderItems;


                context.CartItems.AddRange(firstOrderFungi, firstOrderMargherita);

                foreach (var item in firstOrder.OrderCartItems)
                {
                    firstOrder.TotalAmount = firstOrder.TotalAmount + item.Price;
                    foreach (var cii in item.CartItemIngredients)
                    {
                        firstOrder.TotalAmount = firstOrder.TotalAmount + cii.Price;
                    }
                }

                //var secondOrder = new Order();
                //context.Dishes.AddRange(capricciosa, margherita, hawaii, fungi);

                context.Orders.AddRange(firstOrder);
                studentUser.Orders.Add(firstOrder);
                context.SaveChanges();
            }
        }
    }
}
