//using Microsoft.AspNetCore.Identity;
//using PizzaDeluxe.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using PizzaShop.Data;
//using PizzaShop.Entities;

//namespace PizzaDeluxe.Data
//{
//    public class DbInitializer
//    {
//        public DbInitializer()
//        {

//        }

//        public static void Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
//        {
//            if (!userManager.Users.Any())
//            {
//                var studentUser = new ApplicationUser
//                {
//                    UserName = "student@test.se",
//                    Email = "student@test.se",
//                    Orders = new List<Order>(),
//                    FirstName = "Daniel Oskar",
//                    LastName = "Eliovits",
//                    Address = "Fyren 28",
//                    Zipcode = "12134",
//                    City = "Stockholm"
//                };
//                var studentUserResult = userManager.CreateAsync(studentUser, "Pa$$w0rd").Result;
//                var teacherUser = new ApplicationUser
//                {
//                    UserName = "teacher@test.se",
//                    Email = "teacher@test.se",
//                    Orders = new List<Order>(),
//                    FirstName = "Lärare Oskar",
//                    LastName = "Benny",
//                    Address = "Kroken 02",
//                    Zipcode = "12121",
//                    City = "Göteborg"
//                };
//                var teacherUserResult = userManager.CreateAsync(teacherUser, "Pa$$w0rd").Result;

//                var adminRole = new IdentityRole { Name = "Admin" };
//                var roleResult = roleManager.CreateAsync(adminRole).Result;

//                var adminUser = new ApplicationUser
//                {
//                    UserName = "admin@test.se",
//                    Email = "admin@test.se",
//                    Orders = new List<Order>(),
//                    FirstName = "Super",
//                    LastName = "Admin",
//                    Address = "Adminstreet 05",
//                    Zipcode = "12121",
//                    City = "Uppsala"
//                };
//                var adminUserResult = userManager.CreateAsync(adminUser, "Pa$$w0rd").Result;

//                userManager.AddToRoleAsync(adminUser, "Admin");
//            }

//            if (!context.Dishes.Any())
//            {
//                //INGREDIENTS
//                var cheese = new Ingredient { Name = "Cheese", Price = 5 };
//                var ham = new Ingredient { Name = "Ham", Price = 5 };
//                var tomatoSauce = new Ingredient { Name = "Tomato", Price = 5 };
//                var mushroom = new Ingredient { Name = "Mushroom", Price = 5 };
//                var shrimp = new Ingredient { Name = "Shrimp", Price = 5 };
//                var tuna = new Ingredient { Name = "Tuna", Price = 5 };
//                var pineapple = new Ingredient { Name = "Pineapple", Price = 5 };
//                var curry = new Ingredient { Name = "Curry", Price = 5 };
//                var bacon = new Ingredient { Name = "Bacon", Price = 5 };
//                var banana = new Ingredient { Name = "Banana", Price = 5 };
//                var meat = new Ingredient { Name = "Meat", Price = 5 };
//                var cream = new Ingredient { Name = "Cream", Price = 5 };
//                var onion = new Ingredient { Name = "Onion", Price = 5 };
//                var lettuce = new Ingredient { Name = "Lettuce", Price = 5 };
//                var olives = new Ingredient { Name = "Olives", Price = 5 };
//                var chicken = new Ingredient { Name = "Chicken", Price = 5 };
//                var feta = new Ingredient { Name = "Feta", Price = 5 };
//                var ruccola = new Ingredient { Name = "Ruccola", Price = 5 };
//                var parmigiano = new Ingredient { Name = "Parmigiano", Price = 5 };

//                //DISH CATEGORIES
//                var pizza = new Category { Name = "Pizza" };
//                var salad = new Category { Name = "Salad" };
//                var pasta = new Category { Name = "Pasta" };

//                //DISHES
//                var margherita = new Dish { Name = "Margherita", Price = 89, Category = pizza };
//                var fungi = new Dish { Name = "Fungi", Price = 91, Category = pizza };
//                var capricciosa = new Dish { Name = "Capricciosa", Price = 99, Category = pizza };
//                var hawaii = new Dish { Name = "Hawaii", Price = 99, Category = pizza };
//                var carbonara = new Dish { Name = "Carbonara", Price = 89, Category = pasta };
//                var lasagne = new Dish { Name = "Lasagne", Price = 95, Category = pasta };
//                var pastaConTono = new Dish { Name = "Pasta Con Tono", Price = 92, Category = pasta };
//                var dietSalad = new Dish { Name = "Diet Salad", Price = 29, Category = salad };
//                var greekSalad = new Dish { Name = "Greek Salad", Price = 89, Category = salad };

//                //MARGHERITA PIZZA DISHINGREDIENTS
//                var margheritaCheese = new DishIngredient { Dish = margherita, Ingredient = cheese };
//                var margheritaTomatoSouce = new DishIngredient { Dish = margherita, Ingredient = tomatoSauce };

//                //CAPRICCIOSA PIZZA DISHINGREDIENTS
//                var capricciosaCheese = new DishIngredient { Dish = capricciosa, Ingredient = cheese };
//                var capricciosaHam = new DishIngredient { Dish = capricciosa, Ingredient = ham };
//                var capricciosaTomatoSauce = new DishIngredient { Dish = capricciosa, Ingredient = tomatoSauce };

//                //FUNGI PIZZA DISHINGREDIENTS
//                var fungiTomatoSauce = new DishIngredient { Dish = fungi, Ingredient = tomatoSauce };
//                var fungiCheese = new DishIngredient { Dish = fungi, Ingredient = cheese };
//                var fungiMushroom = new DishIngredient { Dish = fungi, Ingredient = mushroom };

//                //HAWAII PIZZA DISHINGREDIENTS
//                var hawaiiTomatoSauce = new DishIngredient { Dish = hawaii, Ingredient = tomatoSauce };
//                var hawaiiCheese = new DishIngredient { Dish = hawaii, Ingredient = cheese };
//                var hawaiiCurry = new DishIngredient { Dish = hawaii, Ingredient = curry };
//                var hawaiiHam = new DishIngredient { Dish = hawaii, Ingredient = ham };
//                var hawaiiMushroom = new DishIngredient { Dish = hawaii, Ingredient = mushroom };
//                var hawaiiBanana = new DishIngredient { Dish = hawaii, Ingredient = banana };
//                var hawaiiPineapple = new DishIngredient { Dish = hawaii, Ingredient = pineapple };

//                //CARBONARA DISHINGREDIENTS
//                var carbonaraCheese = new DishIngredient { Dish = carbonara, Ingredient = tomatoSauce };
//                var carbonaraCream = new DishIngredient { Dish = carbonara, Ingredient = cream };
//                var carbonaraBacon = new DishIngredient { Dish = carbonara, Ingredient = bacon };

//                //LASAGNE DISHINGREDIENTS
//                var lasagneCheese = new DishIngredient { Dish = lasagne, Ingredient = cheese };
//                var lasagneCream = new DishIngredient { Dish = lasagne, Ingredient = cream };
//                var lasagneMeat = new DishIngredient { Dish = lasagne, Ingredient = meat };

//                //PASTA CON TONO DISHINGREDIENTS
//                var pastaConTonoTomatoSauce = new DishIngredient { Dish = pastaConTono, Ingredient = tomatoSauce };
//                var pastaConTonoTuna = new DishIngredient { Dish = pastaConTono, Ingredient = tuna };
//                var pastaConTonoOnion = new DishIngredient { Dish = pastaConTono, Ingredient = onion };

//                //DIET SALAD DISHINGREDIENTS
//                var dietSaladRuccola = new DishIngredient { Dish = dietSalad, Ingredient = ruccola };
//                var dietSaladParmigiano = new DishIngredient { Dish = dietSalad, Ingredient = parmigiano };

//                //GREEK SALAD DISHINGREDIENTS
//                var greekSaladLettuce = new DishIngredient { Dish = greekSalad, Ingredient = lettuce };
//                var greekSaladOlives = new DishIngredient { Dish = greekSalad, Ingredient = olives };
//                var greekSaladChicken = new DishIngredient { Dish = greekSalad, Ingredient = chicken };
//                var greekSaladFeta = new DishIngredient { Dish = greekSalad, Ingredient = feta };

//                context.Ingredients.AddRange(cheese, tomatoSauce, ham, mushroom, bacon, curry, banana, pineapple, shrimp, tuna, meat, cream, onion);
//                context.Dishes.AddRange(capricciosa, margherita, hawaii, fungi, carbonara, lasagne, pastaConTono, dietSalad, greekSalad);
//                context.DishIngredients.AddRange(capricciosaTomatoSauce, capricciosaCheese,
//                    capricciosaHam, margheritaCheese, margheritaTomatoSouce, fungiMushroom,
//                    fungiCheese, fungiTomatoSauce, hawaiiPineapple, hawaiiBanana, hawaiiCheese,
//                    hawaiiCurry, hawaiiHam, hawaiiMushroom, hawaiiTomatoSauce,
//                    carbonaraCheese, carbonaraCream, carbonaraBacon,
//                    lasagneCheese, lasagneCream, lasagneMeat,
//                    pastaConTonoTuna, pastaConTonoTomatoSauce, pastaConTonoOnion,
//                    dietSaladRuccola, dietSaladParmigiano,
//                    greekSaladChicken, greekSaladFeta, greekSaladLettuce, greekSaladOlives);
//                context.SaveChanges();
//            }
//        }
//    }
//}