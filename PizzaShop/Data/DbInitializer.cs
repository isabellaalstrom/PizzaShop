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
            var studentUser = new ApplicationUser
            {
                UserName = "student@test.se",
                Email = "student@test.se",
                Orders = new List<Order>(),
                Name = "Isabella Gross",
                Address = "Mjärdgatan 8",
                Zipcode = "13343",
                City = "Saltsjöbaden"
            };
            var studentUserResult = userManager.CreateAsync(studentUser, "Pa$$w0rd").Result;
            var teacherUser = new ApplicationUser
            {
                UserName = "teacher@test.se",
                Email = "teacher@test.se"
            };
            var teacherUserResult = userManager.CreateAsync(teacherUser, "Pa$$w0rd").Result;

            var adminRole = new IdentityRole { Name = "Admin" };
            var roleResult = roleManager.CreateAsync(adminRole).Result;

            var adminUser = new ApplicationUser
            {
                UserName = "admin@test.se",
                Email = "admin@test.se"
            };
            var adminUserResult = userManager.CreateAsync(adminUser, "Pa$$w0rd").Result;
            var result = userManager.AddToRoleAsync(adminUser, "Admin");

            //todo refactor
            if (!context.Dishes.Any())
            {
                var cheese = new Ingredient { IngredientName = "Cheese" };
                var ham = new Ingredient { IngredientName = "Ham" };
                var tomatoSauce = new Ingredient { IngredientName = "Tomato" };
                var mushroom = new Ingredient { IngredientName = "Mushroom" };
                var shrimp = new Ingredient { IngredientName = "Shrimp" };
                var tuna = new Ingredient { IngredientName = "Tuna" };
                var pineapple = new Ingredient { IngredientName = "Pineapple" };
                var curry = new Ingredient { IngredientName = "Curry" };
                var bacon = new Ingredient { IngredientName = "Bacon" };
                var banana = new Ingredient { IngredientName = "Banana" };
                var meat = new Ingredient { IngredientName = "Meat" };
                var cream = new Ingredient { IngredientName = "Cream" };
                var onion = new Ingredient { IngredientName = "Onion" };

                var pizzaDishType = new DishType { DishTypeName = "Pizza" };
                var saladDishType = new DishType { DishTypeName = "Salad" };
                var pastaDishType = new DishType { DishTypeName = "Pasta" };
                context.DishTypes.AddRange(pizzaDishType, saladDishType, pastaDishType);


                var margherita = new Dish { DishName = "Margherita", Price = 89, DishType = pizzaDishType };
                var fungi = new Dish { DishName = "Fungi", Price = 91, DishType = pizzaDishType };
                var capricciosa = new Dish { DishName = "Capricciosa", Price = 99, DishType = pizzaDishType };
                var hawaii = new Dish { DishName = "Hawaii", Price = 99, DishType = pizzaDishType };

                var carbonara = new Dish { DishName = "Carbonara", Price = 89, DishType = pastaDishType };
                var lasagne = new Dish { DishName = "Lasagne", Price = 95, DishType = pastaDishType };
                var pastaConTono = new Dish { DishName = "Pasta Con Tono", Price = 92, DishType = pastaDishType };

                var margheritaCheese = new DishIngredient { Dish = margherita, Ingredient = cheese };
                var margheritaTomatoSouce = new DishIngredient { Dish = margherita, Ingredient = tomatoSauce };

                var capricciosaCheese = new DishIngredient { Dish = capricciosa, Ingredient = cheese };
                var capricciosaHam = new DishIngredient { Dish = capricciosa, Ingredient = ham };
                var capricciosaTomatoSauce = new DishIngredient { Dish = capricciosa, Ingredient = tomatoSauce };

                var fungiTomatoSauce = new DishIngredient { Dish = fungi, Ingredient = tomatoSauce };
                var fungiCheese = new DishIngredient { Dish = fungi, Ingredient = cheese };
                var fungiMushroom = new DishIngredient { Dish = fungi, Ingredient = mushroom };

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

                capricciosa.DishIngredients = new List<DishIngredient>();
                capricciosa.DishIngredients.Add(capricciosaTomatoSauce);
                capricciosa.DishIngredients.Add(capricciosaHam);
                capricciosa.DishIngredients.Add(capricciosaCheese);

                margherita.DishIngredients = new List<DishIngredient>();
                margherita.DishIngredients.Add(margheritaCheese);
                margherita.DishIngredients.Add(margheritaTomatoSouce);

                fungi.DishIngredients = new List<DishIngredient>();
                fungi.DishIngredients.Add(fungiMushroom);
                fungi.DishIngredients.Add(fungiCheese);
                fungi.DishIngredients.Add(fungiTomatoSauce);

                hawaii.DishIngredients = new List<DishIngredient>();
                hawaii.DishIngredients.Add(hawaiiTomatoSauce);
                hawaii.DishIngredients.Add(hawaiiCheese);
                hawaii.DishIngredients.Add(hawaiiCurry);
                hawaii.DishIngredients.Add(hawaiiHam);
                hawaii.DishIngredients.Add(hawaiiMushroom);
                hawaii.DishIngredients.Add(hawaiiBanana);
                hawaii.DishIngredients.Add(hawaiiPineapple);

                var firstOrder = new Order();
                firstOrder.OrderDateTime = DateTime.Now;
                firstOrder.User = studentUser;

                var firstOrderHawaii = new OrderDish { Dish = hawaii, Order = firstOrder };
                var firstOrderFungi = new OrderDish { Dish = fungi, Order = firstOrder };

                var firstOrderDishes = new List<OrderDish>();
                firstOrderDishes.Add(firstOrderHawaii);
                firstOrderDishes.Add(firstOrderFungi);
                firstOrder.OrderDishes = firstOrderDishes;

                foreach (var dish in firstOrder.OrderDishes)
                {
                    firstOrder.TotalAmount = firstOrder.TotalAmount + dish.Dish.Price;
                }
                //firstOrder.TotalAmount = firstOrder.OrderDishes.ForEach(dish => dish.Dish.Price += dish.Dish.Price);

                var secondOrder = new Order();

                studentUser.Orders.Add(firstOrder);

                context.DishTypes.AddRange(pizzaDishType, saladDishType, pastaDishType);
                context.Orders.AddRange(firstOrder);
                context.OrderDishes.AddRange(firstOrderFungi, firstOrderHawaii);
                context.Ingredients.AddRange(cheese, tomatoSauce, ham, mushroom, bacon, curry, banana, pineapple, shrimp, tuna);
                context.Dishes.AddRange(capricciosa, margherita, hawaii, fungi);
                context.DishIngredients.AddRange(capricciosaTomatoSauce, capricciosaCheese,
                    capricciosaHam, margheritaCheese, margheritaTomatoSouce, fungiMushroom,
                    fungiCheese, fungiTomatoSauce, hawaiiPineapple, hawaiiBanana, hawaiiCheese,
                    hawaiiCurry, hawaiiHam, hawaiiMushroom, hawaiiTomatoSauce, carbonaraCheese,
                    carbonaraCream, carbonaraBacon, lasagneCheese, lasagneCream, lasagneMeat,
                    pastaConTonoTuna, pastaConTonoTomatoSauce, pastaConTonoOnion);
                context.SaveChanges();
            }
        }
    }
}
