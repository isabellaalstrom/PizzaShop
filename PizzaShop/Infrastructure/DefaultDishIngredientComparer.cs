using System.Collections.Generic;
using PizzaShop.Entities;

namespace PizzaShop.Infrastructure
{
    public class DefaultDishIngredientComparer : IEqualityComparer<DishIngredient>
    {
        public bool Equals(DishIngredient x, DishIngredient y)
        {
            return x.Ingredient.IngredientName == y.Ingredient.IngredientName;
        }

        public int GetHashCode(DishIngredient obj)
        {
            return obj.Ingredient.IngredientName.GetHashCode();
        }
    }
}
