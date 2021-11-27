using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodPlanner.Enums;
using FoodPlanner.Data;

namespace FoodPlanner.Helper
{
    public static class DataHandling
    {
        public static void Refresh(object objectToRefresh, REFRESH_TYPE table)
        {
            // Refresh element
            Data.DB.DBHandler.RefreshTable(table);
        }

        public static bool CheckForIngredientExistance(string ingredient)
        {
            List<Ingredient> ingredients = new List<Ingredient>();      // TODO Replace with actual list of ALL ingredients (DB)
            foreach (Ingredient i in ingredients)
                if (i.Name == ingredient) 
                    return true;

            // TODO Show ingredient not existent error
            return false;
        }

        public static bool CheckForIngredientExistance(Ingredient ingredient)
        {
            List<Ingredient> ingredients = new List<Ingredient>();      // TODO Replace with actual list of ALL ingredients (DB)
            foreach (Ingredient i in ingredients)
                if (i.Name == ingredient.Name) 
                    return true;

            // TODO Show ingredient not existent error
            return false;
        }

        public static Ingredient GetIngredient(string ingredient)
        {
            List<Ingredient> ingredients = new List<Ingredient>();      // TODO Replace with actual list of ALL ingredients
            foreach (Ingredient i in ingredients)
            {
                if (i.Name == ingredient) return i;
            }

            // TODO Show ingredient not existent error
            return null;
        }
    }
}
