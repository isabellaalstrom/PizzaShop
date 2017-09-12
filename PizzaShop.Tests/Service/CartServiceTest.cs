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
        [Fact]
        public void AddToCart_Can_Add_New_Items()
        {
            var cart = new Cart();
            // Arrange
            var mockDish = new Dish
            {
                DishId = 1,
                DishName = "P1",
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
                }
            }
            };
            var mockAccessor = new Mock<IHttpContextAccessor>();
            var mockCart = new Mock<CartService>(mockAccessor.Object);
            mockCart.Setup(x => x.GetCart()).Returns(cart);
            mockCart.Setup(x => x.SaveCart(cart)).Returns(true);
            // Act
            mockCart.Object.AddToCart(mockDish, 1);
            var results = mockCart.Object.GetCart().CartItems.ToArray();
            // Assert
            Assert.Equal(mockDish, results[0].Dish);
            Assert.Equal(results.Length, 1);
            Assert.NotEmpty(results);
        }

        [Fact]
        public void ComputeTotalValue_Margherita_Returns_89()
        {
            // Arrange
            var cart = new Cart();
            var mockCartItems = new List<CartItem>()
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
            var mockCart = new Mock<CartService>(mockAccessor.Object);
            mockCart.Setup(x => x.GetCart()).Returns(cart);
            // Act
            var results = mockCart.Object.ComputeTotalValue();
            // Assert
            Assert.True(results == 89);
        }
    }
}
