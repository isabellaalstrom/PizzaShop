using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using PizzaShop.Controllers;
using PizzaShop.Entities;
using PizzaShop.Services;
using Xunit;
using Moq;
using PizzaShop.Data;
using PizzaShop.Models;

namespace PizzaShop.Tests.Service
{
    public class CartServiceTest
    {
        public ServiceProvider ServiceProvider;

        public CartServiceTest()
        {
            var efServiceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(b => b.UseInMemoryDatabase("Pizzadatabas")
                .UseInternalServiceProvider(efServiceProvider));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddTransient<CartService>();
            services.AddTransient<CartItemService>();
            services.AddTransient<IngredientService>();
            services.AddTransient<ISession, TestSession>();
            services.AddTransient<IEmailSender, FakeEmailSender>();

            ServiceProvider = services.BuildServiceProvider();
            services.AddSingleton<IServiceProvider>(ServiceProvider);
        }

        [Fact]
        public void AddToCart_Can_Add_New_Items()
        {
            var context = ServiceProvider.GetService<ApplicationDbContext>();

            // Arrange
            var mockDish = new Dish
            {
                DishId = 1,
                DishName = "Margherita",
                Price = 89,
                DishTypeId = 1,
                DishIngredients = new List<DishIngredient>
                {
                    new DishIngredient
                    {
                        DishId = 1, Ingredient = new Ingredient
                        {
                            IngredientName = "Cheese", IngredientId = 1, Price = 5
                        }
                    },
                    new DishIngredient
                    {
                        DishId = 1, Ingredient = new Ingredient
                        {
                            IngredientName = "Tomato Sauce", IngredientId = 2, Price = 5
                        }
                    }
                }
            };
            context.Dishes.Add(mockDish);
            context.SaveChanges();

            var _cartService = ServiceProvider.GetService<CartService>();
            _cartService.AddToCart(mockDish, 1);
            // Act
            var results = _cartService.GetCart().CartItems.ToArray();
            // Assert
            Assert.Equal(mockDish.DishName, results[0].Dish.DishName);
            Assert.Equal(results.Length, 1);
            Assert.NotEmpty(results);
        }

        [Fact]
        public void ComputeTotalValue_Margherita_Returns_Correct_Price()
        {
            // Arrange
            var price = 89;
            var cart = new Cart();
            var mockCartItems = new List<CartItem>
            {
                new CartItem
                {
                    CartItemId = 1,
                    DishId = 1,
                    Price = price,
                    CartId = Guid.NewGuid(),
                    Quantity = 1,
                    CartItemIngredients = new List<CartItemIngredient>
                    {
                        new CartItemIngredient
                        {
                            CartItemId = 1,
                            IngredientName = "Cheese",
                            IsOriginalIngredient = true,
                            Price = 0
                        },
                        new CartItemIngredient
                        {
                            CartItemId = 1,
                            IngredientName = "Tomato",
                            IsOriginalIngredient = true,
                            Price = 0
                        }
                    }
                }
            };
            cart.CartItems = mockCartItems;
            var mockCartService = new Mock<CartService>(new TestSession());
            mockCartService.Setup(x => x.GetCart()).Returns(cart);
            // Act
            var results = mockCartService.Object.ComputeTotalValue();
            // Assert
            Assert.True(results == price);
        }

        [Fact]
        public void ComputeTotalValue_Margherita_Extra_Ingredient_Returns_Correct_Price()
        {
            // Arrange
            var price = 99;
            var extraIngredientPrice = 10;
            var expectedPrice = price + extraIngredientPrice;
            var cart = new Cart();
            var mockCartItems = new List<CartItem>
            {
                new CartItem
                {
                    CartItemId = 1,
                    DishId = 1,
                    Price = price,
                    CartId = Guid.NewGuid(),
                    Quantity = 1,
                    CartItemIngredients = new List<CartItemIngredient>
                    {
                        new CartItemIngredient
                        {
                            CartItemId = 1,
                            IngredientName = "Cheese",
                            IsOriginalIngredient = true,
                            Price = 0
                        },
                        new CartItemIngredient
                        {
                            CartItemId = 1,
                            IngredientName = "Tomato",
                            IsOriginalIngredient = true,
                            Price = 0
                        },
                        new CartItemIngredient
                        {
                            CartItemId = 1,
                            IngredientName = "Ham",
                            IsOriginalIngredient = false,
                            Price = extraIngredientPrice
                        }
                    }
                }
            };
            cart.CartItems = mockCartItems;
            var mockCartService = new Mock<CartService>(new TestSession());
            mockCartService.Setup(x => x.GetCart()).Returns(cart);
            // Act
            var results = mockCartService.Object.ComputeTotalValue();
            // Assert
            Assert.True(results == expectedPrice);
        }

        [Fact]
        public void ComputeTotalValue_Two_Margherita_Extra_Ingredient_Returns_Correct_Price()
        {
            // Arrange
            var price = 99;
            var extraIngredientPrice = 10;
            var expectedPrice = (price + extraIngredientPrice) * 2;
            var cart = new Cart();
            var mockCartItems = new List<CartItem>
            {
                new CartItem
                {
                    CartItemId = 1,
                    DishId = 1,
                    Price = price,
                    CartId = Guid.NewGuid(),
                    Quantity = 2,
                    CartItemIngredients = new List<CartItemIngredient>
                    {
                        new CartItemIngredient
                        {
                            CartItemId = 1,
                            IngredientName = "Cheese",
                            IsOriginalIngredient = true,
                            Price = 0
                        },
                        new CartItemIngredient
                        {
                            CartItemId = 1,
                            IngredientName = "Tomato",
                            IsOriginalIngredient = true,
                            Price = 0
                        },
                        new CartItemIngredient
                        {
                            CartItemId = 1,
                            IngredientName = "Ham",
                            IsOriginalIngredient = false,
                            Price = extraIngredientPrice
                        }
                    }
                }
            };
            cart.CartItems = mockCartItems;
            var mockCartService = new Mock<CartService>(new TestSession());
            mockCartService.Setup(x => x.GetCart()).Returns(cart);
            // Act
            var results = mockCartService.Object.ComputeTotalValue();
            // Assert
            Assert.True(results == expectedPrice);
        }

        [Fact]
        public void ComputeTotalValue_Margherita_Excluded_Original_Ingredient_Returns_Correct_Price()
        {
            // Arrange
            var price = 89;
            var cart = new Cart();
            var mockCartItems = new List<CartItem>
            {
                new CartItem
                {
                    CartItemId = 1,
                    DishId = 1,
                    Price = price,
                    CartId = Guid.NewGuid(),
                    Quantity = 1,
                    CartItemIngredients = new List<CartItemIngredient>
                    {
                        new CartItemIngredient
                        {
                            CartItemId = 1,
                            IngredientName = "Cheese",
                            IsOriginalIngredient = true,
                            Price = 0
                        }
                    }
                }
            };
            cart.CartItems = mockCartItems;
            var mockCartService = new Mock<CartService>(new TestSession());
            mockCartService.Setup(x => x.GetCart()).Returns(cart);
            // Act
            var results = mockCartService.Object.ComputeTotalValue();
            // Assert
            Assert.True(results == price);
        }

        [Fact]
        public void ComputeTotalValue_Margherita_Excluded_Original_Ingredient_And_Extra_Ingredient_Returns_Correct_Price()
        {
            // Arrange
            var price = 99;
            var extraIngredientPrice = 10;
            var expectedPrice = price + extraIngredientPrice;
            var cart = new Cart();
            var mockCartItems = new List<CartItem>
            {
                new CartItem
                {
                    CartItemId = 1,
                    DishId = 1,
                    Price = price,
                    CartId = Guid.NewGuid(),
                    Quantity = 1,
                    CartItemIngredients = new List<CartItemIngredient>
                    {
                        new CartItemIngredient
                        {
                            CartItemId = 1,
                            IngredientName = "Cheese",
                            IsOriginalIngredient = true,
                            Price = 0
                        },
                        new CartItemIngredient
                        {
                            CartItemId = 1,
                            IngredientName = "Ham",
                            IsOriginalIngredient = false,
                            Price = extraIngredientPrice
                        }
                    }
                }
            };
            cart.CartItems = mockCartItems;
            var mockCartService = new Mock<CartService>(new TestSession());
            mockCartService.Setup(x => x.GetCart()).Returns(cart);
            // Act
            var results = mockCartService.Object.ComputeTotalValue();
            // Assert
            Assert.True(results == expectedPrice);
        }
    }
}
