using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PizzaShop.Controllers;
using PizzaShop.Entities;
using PizzaShop.Services;
using Xunit;
using Moq;
using PizzaShop.Models;

namespace PizzaShop.Tests.Service
{
    public class CartServiceTest
    {
        //[Fact]
        //public void AddToCart_Can_Add_New_Items()
        //{
        //    var cart = new Cart();
        //    // Arrange
        //    var mockDish = new Dish
        //    {
        //        DishId = 1,
        //        DishName = "Margherita",
        //        Price = 89,
        //        DishTypeId = 1,
        //        DishIngredients = new List<DishIngredient>
        //    {
        //        new DishIngredient
        //        {
        //            DishId = 1, Ingredient = new Ingredient
        //            {
        //                IngredientName = "Cheese", IngredientId = 1, Price = 5
        //            }
        //        },
        //        new DishIngredient
        //        {
        //            DishId = 1, Ingredient = new Ingredient
        //            {
        //                IngredientName = "Tomato Sauce", IngredientId = 2, Price = 5
        //            }
        //        }
        //    }
        //    };
        //    var mockAccessor = new Mock<IHttpContextAccessor>();
        //    var mockCartService = new Mock<CartService>(mockAccessor.Object);
        //    mockCartService.Setup(x => x.GetCart()).Returns(cart);
        //    mockCartService.Setup(x => x.SaveCart(cart)).Returns(true);
        //    mockCartService.Object.AddToCart(mockDish, 1);
        //    var _cartService = new CartService(mockAccessor.Object);
        //    // Act
        //    //var results = mockCartService.Object.GetCart().CartItems.ToArray();
        //    var results = _cartService.GetCart().CartItems.ToArray();
        //    // Assert
        //    Assert.Equal(mockDish, results[0].Dish);
        //    Assert.Equal(results.Length, 1);
        //    Assert.NotEmpty(results);
        //}

        [Fact]
        public void ComputeTotalValue_Margherita_Returns_89()
        {
            // Arrange
            var cart = new Cart();
            var mockCartItems = new List<CartItem>
            {
                new CartItem
                {
                    CartItemId = 1,
                    DishId = 1,
                    Price = 89,
                    CartId = 0,
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
            var mockAccessor = new Mock<IHttpContextAccessor>();
            var mockCartService = new Mock<CartService>(mockAccessor.Object);
            mockCartService.Setup(x => x.GetCart()).Returns(cart);
            // Act
            var results = mockCartService.Object.ComputeTotalValue();
            // Assert
            Assert.True(results == 89);
        }

        [Fact]
        public void ComputeTotalValue_Margherita_Extra_Ingredient_Returns_99()
        {
            // Arrange
            var cart = new Cart();
            var mockCartItems = new List<CartItem>
            {
                new CartItem
                {
                    CartItemId = 1,
                    DishId = 1,
                    Price = 89,
                    CartId = 0,
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
                            Price = 10
                        }
                    }
                }
            };
            cart.CartItems = mockCartItems;
            var mockAccessor = new Mock<IHttpContextAccessor>();
            var mockCartServiceService = new Mock<CartService>(mockAccessor.Object);
            mockCartServiceService.Setup(x => x.GetCart()).Returns(cart);
            // Act
            var results = mockCartServiceService.Object.ComputeTotalValue();
            // Assert
            Assert.True(results == 99);
        }

        [Fact]
        public void ComputeTotalValue_Two_Margherita_Extra_Ingredient_Returns_198()
        {
            // Arrange
            var cart = new Cart();
            var mockCartItems = new List<CartItem>
            {
                new CartItem
                {
                    CartItemId = 1,
                    DishId = 1,
                    Price = 89,
                    CartId = 0,
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
                            Price = 10
                        }
                    }
                }
            };
            cart.CartItems = mockCartItems;
            var mockAccessor = new Mock<IHttpContextAccessor>();
            var mockCartService = new Mock<CartService>(mockAccessor.Object);
            mockCartService.Setup(x => x.GetCart()).Returns(cart);
            // Act
            var results = mockCartService.Object.ComputeTotalValue();
            // Assert
            Assert.True(results == 198);
        }

        [Fact]
        public void ComputeTotalValue_Margherita_Excluded_Original_Ingredient_Returns_89()
        {
            // Arrange
            var cart = new Cart();
            var mockCartItems = new List<CartItem>
            {
                new CartItem
                {
                    CartItemId = 1,
                    DishId = 1,
                    Price = 89,
                    CartId = 0,
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
            var mockAccessor = new Mock<IHttpContextAccessor>();
            var mockCartService = new Mock<CartService>(mockAccessor.Object);
            mockCartService.Setup(x => x.GetCart()).Returns(cart);
            // Act
            var results = mockCartService.Object.ComputeTotalValue();
            // Assert
            Assert.True(results == 89);
        }

        [Fact]
        public void ComputeTotalValue_Margherita_Excluded_Original_Ingredient_And_Extra_Ingredient_Returns_99()
        {
            // Arrange
            var cart = new Cart();
            var mockCartItems = new List<CartItem>
            {
                new CartItem
                {
                    CartItemId = 1,
                    DishId = 1,
                    Price = 89,
                    CartId = 0,
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
                            Price = 10
                        }
                    }
                }
            };
            cart.CartItems = mockCartItems;
            var mockAccessor = new Mock<IHttpContextAccessor>();
            var mockCartService = new Mock<CartService>(mockAccessor.Object);
            mockCartService.Setup(x => x.GetCart()).Returns(cart);
            // Act
            var results = mockCartService.Object.ComputeTotalValue();
            // Assert
            Assert.True(results == 99);
        }
    }
}
