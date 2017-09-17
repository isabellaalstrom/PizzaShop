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
        public void GetItemPrice_No_Ingredients_Returns_Correct_Price()
        {
            //Arrange
            var price = 89;
            var item = new CartItem
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
            var cartItemService = new CartItemService(dbContext);
            //Act
            var result = cartItemService.GetItemPrice(item);
            //Assert
            Assert.Equal(result, price);
        }

        [Fact]
        public void GetItemPrice_Margherita_Extra_Ingredient_Returns_Correct_Price()
        {
            //Arrange
            var price = 89;
            var extraIngredientPrice = 10;
            var item = new CartItem
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
                        IngredientName = "Tomato Sauce",
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
            };
            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("Test")
                .Options;
            var dbContext = new ApplicationDbContext(dbOptions);
            var cartItemService = new CartItemService(dbContext);
            //Act
            var result = cartItemService.GetItemPrice(item);
            //Assert
            Assert.Equal(result, price + extraIngredientPrice);
        }
    }
}
