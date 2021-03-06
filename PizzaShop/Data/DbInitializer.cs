﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using PizzaShop.Entities;

namespace PizzaShop.Data
{
    public class DbInitializer
    {
        public static void Initializer(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!userManager.Users.Any())
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

                if (!roleManager.Roles.Any(r => r.Name == "Admin"))
                {
                    var adminRole = new IdentityRole { Name = "Admin" };
                    var roleResult = roleManager.CreateAsync(adminRole).Result;
                }

                var addToRoleResult = userManager.AddToRoleAsync(adminUser, "Admin").Result;
            }

            if (!context.Dishes.Any())
            {
                #region INGREDIENTS
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
                #endregion

                #region DISH TYPES

                var pizzaDishType = new DishType { DishTypeName = "Pizza" };
                var saladDishType = new DishType { DishTypeName = "Salad" };
                var pastaDishType = new DishType { DishTypeName = "Pasta" };
                #endregion

                #region DISHES
                var margherita = new Dish { DishName = "Margherita", Price = 89, DishType = pizzaDishType };
                var fungi = new Dish { DishName = "Fungi", Price = 91, DishType = pizzaDishType };
                var capricciosa = new Dish { DishName = "Capricciosa", Price = 99, DishType = pizzaDishType };
                var hawaii = new Dish { DishName = "Hawaii", Price = 99, DishType = pizzaDishType };
                var carbonara = new Dish { DishName = "Carbonara", Price = 89, DishType = pastaDishType };
                var lasagne = new Dish { DishName = "Lasagne", Price = 95, DishType = pastaDishType };
                var pastaConTono = new Dish { DishName = "Pasta Con Tono", Price = 92, DishType = pastaDishType };
                var greekSalad = new Dish { DishName = "Greek Salad", Price = 79, DishType = saladDishType };
                #endregion

                #region DISH INGREDIENTS
                var margheritaCheese = new DishIngredient { Dish = margherita, Ingredient = cheese };
                var margheritaTomatoSouce = new DishIngredient { Dish = margherita, Ingredient = tomatoSauce };

                var fungiTomatoSauce = new DishIngredient { Dish = fungi, Ingredient = tomatoSauce };
                var fungiCheese = new DishIngredient { Dish = fungi, Ingredient = cheese };
                var fungiMushroom = new DishIngredient { Dish = fungi, Ingredient = mushroom };

                var capricciosaCheese = new DishIngredient { Dish = capricciosa, Ingredient = cheese };
                var capricciosaHam = new DishIngredient { Dish = capricciosa, Ingredient = ham };
                var capricciosaTomatoSauce = new DishIngredient { Dish = capricciosa, Ingredient = tomatoSauce };

                var hawaiiTomatoSauce = new DishIngredient { Dish = hawaii, Ingredient = tomatoSauce };
                var hawaiiCheese = new DishIngredient { Dish = hawaii, Ingredient = cheese };
                var hawaiiCurry = new DishIngredient { Dish = hawaii, Ingredient = curry };
                var hawaiiHam = new DishIngredient { Dish = hawaii, Ingredient = ham };
                var hawaiiMushroom = new DishIngredient { Dish = hawaii, Ingredient = mushroom };
                var hawaiiBanana = new DishIngredient { Dish = hawaii, Ingredient = banana };
                var hawaiiPineapple = new DishIngredient { Dish = hawaii, Ingredient = pineapple };

                var carbonaraCheese = new DishIngredient { Dish = carbonara, Ingredient = tomatoSauce };
                var carbonaraCream = new DishIngredient { Dish = carbonara, Ingredient = cream };
                var carbonaraBacon = new DishIngredient { Dish = carbonara, Ingredient = bacon };

                var lasagneCheese = new DishIngredient { Dish = lasagne, Ingredient = cheese };
                var lasagneCream = new DishIngredient { Dish = lasagne, Ingredient = cream };
                var lasagneMeat = new DishIngredient { Dish = lasagne, Ingredient = meat };

                var pastaConTonoTomatoSauce = new DishIngredient { Dish = pastaConTono, Ingredient = tomatoSauce };
                var pastaConTonoTuna = new DishIngredient { Dish = pastaConTono, Ingredient = tuna };
                var pastaConTonoOnion = new DishIngredient { Dish = pastaConTono, Ingredient = onion };

                var greekSaladLettuce = new DishIngredient { Dish = greekSalad, Ingredient = lettuce };
                var greekSaladOlives = new DishIngredient { Dish = greekSalad, Ingredient = olives };
                var greekSaladChicken = new DishIngredient { Dish = greekSalad, Ingredient = chicken };
                var greekSaladFeta = new DishIngredient { Dish = greekSalad, Ingredient = feta };
                var greekSaladTomatoes = new DishIngredient { Dish = greekSalad, Ingredient = tomato };
                #endregion

                #region ORDER
                //var firstOrderUser = userManager.Users.FirstOrDefault(user => user.UserName == "student@test.se");

                //var firstOrder = new Order();
                //firstOrder.OrderDateTime = DateTime.Now;
                //firstOrder.User = firstOrderUser;
                //firstOrder.OrderId = 1;

                //var firstOrderMargherita = new CartItem
                //{
                //    Dish = margherita,
                //    Order = firstOrder,
                //    IsModified = true,
                //    Price = margherita.Price,
                //    CartItemName = margherita.DishName
                //};
                //var cartItemMCheese = new CartItemIngredient
                //{
                //    CartItem = firstOrderMargherita,
                //    IngredientName = "Cheese",
                //    IsOriginalIngredient = true
                //};
                //var cartItemMTomatoSauce = new CartItemIngredient
                //{
                //    CartItem = firstOrderMargherita,
                //    IngredientName = "Tomato Sauce",
                //    IsOriginalIngredient = true
                //};
                //var cartItemMMushroom = new CartItemIngredient
                //{
                //    CartItem = firstOrderMargherita,
                //    IngredientName = "Mushroom",
                //    IsOriginalIngredient = false,
                //    Price = 5
                //};

                //firstOrderMargherita.CartItemIngredients =
                //    new List<CartItemIngredient> { cartItemMMushroom, cartItemMCheese, cartItemMTomatoSauce };

                //var firstOrderFungi = new CartItem { Dish = fungi, Order = firstOrder, IsModified = false, Price = fungi.Price, CartItemName = fungi.DishName };
                //var cartItemFCheese = new CartItemIngredient
                //{
                //    CartItem = firstOrderFungi,
                //    IngredientName = "Cheese",
                //    IsOriginalIngredient = true
                //};
                //var cartItemFTomatoSauce = new CartItemIngredient
                //{
                //    CartItem = firstOrderFungi,
                //    IngredientName = "Tomato Sauce",
                //    IsOriginalIngredient = true
                //};
                //var cartItemFMushroom = new CartItemIngredient
                //{
                //    CartItem = firstOrderFungi,
                //    IngredientName = "Mushroom",
                //    IsOriginalIngredient = true
                //};
                //firstOrderFungi.CartItemIngredients = new List<CartItemIngredient> { cartItemFMushroom, cartItemFCheese, cartItemFTomatoSauce };

                //var firstOrderItems = new List<CartItem> { firstOrderMargherita, firstOrderFungi };
                //firstOrder.OrderCartItems = firstOrderItems;

                //foreach (var item in firstOrder.OrderCartItems)
                //{
                //    firstOrder.TotalAmount = firstOrder.TotalAmount + item.Price;
                //    foreach (var cii in item.CartItemIngredients)
                //    {
                //        firstOrder.TotalAmount = firstOrder.TotalAmount + cii.Price;
                //    }
                //}
                #endregion

                #region ADD TO CONTEXT
                context.Ingredients.AddRange(cheese, tomatoSauce, ham, mushroom, bacon, curry, banana,
                    pineapple, shrimp, tuna, lettuce, olives, chicken, feta, arugula, parmigiano, tomato);
                context.DishTypes.AddRange(pizzaDishType, saladDishType, pastaDishType);
                context.DishIngredients.AddRange(capricciosaTomatoSauce, capricciosaCheese,
                    capricciosaHam, margheritaCheese, margheritaTomatoSouce, fungiMushroom,
                    fungiCheese, fungiTomatoSauce, hawaiiPineapple, hawaiiBanana, hawaiiCheese,
                    hawaiiCurry, hawaiiHam, hawaiiMushroom, hawaiiTomatoSauce, carbonaraCheese,
                    carbonaraCream, carbonaraBacon, lasagneCheese, lasagneCream, lasagneMeat,
                    pastaConTonoTuna, pastaConTonoTomatoSauce, pastaConTonoOnion,
                    greekSaladChicken, greekSaladFeta, greekSaladLettuce, greekSaladOlives, greekSaladTomatoes);
                //context.CartItems.AddRange(firstOrderFungi, firstOrderMargherita);
                //context.Orders.AddRange(firstOrder);
                //firstOrderUser.Orders.Add(firstOrder);
                context.SaveChanges();
                #endregion
            }
        }
    }
}
