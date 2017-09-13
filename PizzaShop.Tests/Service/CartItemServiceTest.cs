using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Moq;
using PizzaShop.Data;
using PizzaShop.Entities;
using PizzaShop.Services;
using Xunit;

namespace PizzaShop.Tests.Service
{
    public class CartItemServiceTest
    {
        [Fact]
        public void GetItemPrice_Margherita_Returns_89()
        {
            //Arrange
            var item = new CartItem
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
                        IngredientName = "Tomato Sauce",
                        IsOriginalIngredient = true,
                        Price = 0
                    }
                }
            };
            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("Test")
                .Options;
            var dbContext = new ApplicationDbContext(dbOptions);
            var mockCartItemService = new Mock<CartItemService>(dbContext);
            //Act
            var result = mockCartItemService.Object.GetItemPrice(item);
            //Assert
            Assert.Equal(result, 89);
        }

        [Fact]
        public void GetItemPrice_Margherita_Extra_Ingredient_Returns_99()
        {
            //Arrange
            var item = new CartItem
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
                        IngredientName = "Tomato Sauce",
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
            };
            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("Test")
                .Options;
            var dbContext = new ApplicationDbContext(dbOptions);
            var mockCartItemService = new Mock<CartItemService>(dbContext);
            //Act
            var result = mockCartItemService.Object.GetItemPrice(item);
            //Assert
            Assert.Equal(result, 99);
        }
    }
}
