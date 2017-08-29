using System;
using System.Linq;
using PizzaShop.Models;
using Xunit;

namespace PizzaShop.Tests
{
    public class CartTests
    {
        [Fact]
        public void Can_Add_New_Items()
        {
            // Arrange
            Dish p1 = new Dish { DishId = 1, DishName = "P1" };
            Dish p2 = new Dish { DishId = 2, DishName = "P2" };
            Cart target = new Cart();
            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            CartItem[] results = target.Items.ToArray();
            // Assert
            Assert.Equal(2, results.Length);
            Assert.Equal(p1, results[0].Dish);
            Assert.Equal(p2, results[1].Dish);
}
        [Fact]
        public void Can_Add_Quantity_For_Existing_Items()
        {
            // Arrange
            Dish p1 = new Dish { DishId = 1, DishName = "P1" };
            Dish p2 = new Dish { DishId = 2, DishName = "P2" };
            Cart target = new Cart();
            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);
            CartItem[] results = target.Items
                .OrderBy(c => c.Dish.DishId).ToArray();
            // Assert
            Assert.Equal(2, results.Length);
            Assert.Equal(11, results[0].Quantity);
            Assert.Equal(1, results[1].Quantity);
        }

        [Fact]
        public void Can_Remove_Line()
        {
            // Arrange - create some test Dishs
            Dish p1 = new Dish { DishId = 1, DishName = "P1" };
            Dish p2 = new Dish { DishId = 2, DishName = "P2" };
            Dish p3 = new Dish { DishId = 3, DishName = "P3" };

            // Arrange - create a new cart
            Cart target = new Cart();
            // Arrange - add some Dishs to the cart
            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);
            // Act
            target.RemoveItem(p2);
            // Assert
            Assert.Equal(0, target.Items.Count(c => c.Dish == p2));
            Assert.Equal(2, target.Items.Count());
        }
    }
}
