using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodPlanner.Enums;
using FoodPlanner.Helper;

namespace FoodPlanner.Data
{
    public class Buylist
    {
        private Dictionary<Ingredient, KeyValuePair<AMOUNT_TYPE, double>> items;       

        public Buylist()
        {
            this.items = new Dictionary<Ingredient, KeyValuePair<AMOUNT_TYPE, double>>();
            // TODO: Refresh
        }

        public Buylist(Dictionary<Ingredient, KeyValuePair<AMOUNT_TYPE, double>> entries)
        {
            this.items = entries;
            // TODO: Refresh
        }

        public List<string> GetBuyList()
        {
            List<string> buyList = new List<string>() { String.Format("Buy List - {0}", DateTime.Now) };

            if (items.Count == 0)
            {
                buyList.Add("No entries");
                return buyList;
            }

            foreach (KeyValuePair<Ingredient, KeyValuePair<AMOUNT_TYPE, double>> p in items)
                buyList.Add(String.Format("{0,-30} {1, +5} {2, -10}", p.Key, p.Value.Value.ToString(), p.Value.Key));

            return buyList;
        }

        #region ADD

        public void AddItem(KeyValuePair<Ingredient, KeyValuePair<AMOUNT_TYPE, double>> item)
        {
            double oldValue = 0.00;
            foreach (KeyValuePair<Ingredient, KeyValuePair<AMOUNT_TYPE, double>> p in items)
            {
                if (p.Key == item.Key)
                {
                    oldValue = p.Value.Value;
                    items.Remove(p.Key);
                    break;
                }
            }

            items.Add(item.Key, new KeyValuePair<AMOUNT_TYPE, double>(item.Value.Key, (oldValue == 0.00 ? item.Value.Value : oldValue + item.Value.Value)));
            // TODO: Refresh
            return;
        }

        public void AddItem(string ingredient, double value, AMOUNT_TYPE type)
        {
            Ingredient i = DataHandling.GetIngredient(ingredient);
            if (i == null)
                return;

            AddItem(new KeyValuePair<Ingredient, KeyValuePair<AMOUNT_TYPE, double>>(i, new KeyValuePair<AMOUNT_TYPE, double>(type, value)));      
        }

        #endregion

        #region REMOVE
        public void RemoveItem(Ingredient ingredient)
        {
            foreach(KeyValuePair<Ingredient, KeyValuePair<AMOUNT_TYPE, double>> p in items)
            {
                if (p.Key == ingredient)
                    items.Remove(p.Key);
            }

            // TODO: Refresh
        }

        public void RemoveItem(string ingredient)
        {
            Ingredient i = DataHandling.GetIngredient(ingredient);
            if (i == null)
                return;

            RemoveItem(i);
        }

        public void ReduceAmountOfIngredient(Ingredient ingredient, double amount)
        {
            foreach (KeyValuePair<Ingredient, KeyValuePair<AMOUNT_TYPE, double>> p in items)
            {
                if (p.Key == ingredient)
                {
                    double newValue = p.Value.Value - amount;
                    AMOUNT_TYPE type = p.Value.Key;

                    RemoveItem(p.Key);

                    if (newValue > 0.00)
                        items.Add(ingredient, new KeyValuePair<AMOUNT_TYPE, double>(type, newValue));

                    // TODO: Refresh
                    return;
                }                    
            }
            // TODO: not found
        }

        public void ReduceAmountOfIngredient(string ingredient, double amount)
        {
            Ingredient i = DataHandling.GetIngredient(ingredient);
            if (i == null)
                return;

            ReduceAmountOfIngredient(i, amount);
        }

        #endregion

    }
}
