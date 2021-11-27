﻿using System;
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
        public Dictionary<Ingredient, KeyValuePair<AMOUNT_TYPE, double>> Items { get; set; }           

        public Buylist()
        {
            this.Items = new Dictionary<Ingredient, KeyValuePair<AMOUNT_TYPE, double>>();
            Refresh();
        }

        public Buylist(Dictionary<Ingredient, KeyValuePair<AMOUNT_TYPE, double>> entries)
        {
            this.Items = entries;
            Refresh();
        }

        public List<string> GetBuyList()
        {
            List<string> buyList = new List<string>() { String.Format("Buy List - {0}", DateTime.Now) };

            if (Items.Count == 0)
            {
                buyList.Add("No entries");
                return buyList;
            }

            foreach (KeyValuePair<Ingredient, KeyValuePair<AMOUNT_TYPE, double>> p in Items)
                buyList.Add(String.Format("{0,-30} {1, +5} {2, -10}", p.Key, p.Value.Value.ToString(), p.Value.Key));

            return buyList;
        }

        #region ADD

        public void AddItem(KeyValuePair<Ingredient, KeyValuePair<AMOUNT_TYPE, double>> item)
        {
            double oldValue = 0.00;
            foreach (KeyValuePair<Ingredient, KeyValuePair<AMOUNT_TYPE, double>> p in Items)
            {
                if (p.Key == item.Key)
                {
                    oldValue = p.Value.Value;
                    Items.Remove(p.Key);
                    break;
                }
            }

            Items.Add(item.Key, new KeyValuePair<AMOUNT_TYPE, double>(item.Value.Key, (oldValue == 0.00 ? item.Value.Value : oldValue + item.Value.Value)));
            Refresh();
            return;
        }

        public void AddItem(string ingredient, double value, AMOUNT_TYPE type)
        {
            Ingredient i = IngredientHelper.GetIngredient(ingredient);
            if (i == null)
                return;

            AddItem(new KeyValuePair<Ingredient, KeyValuePair<AMOUNT_TYPE, double>>(i, new KeyValuePair<AMOUNT_TYPE, double>(type, value)));      
        }

#endregion

        #region REMOVE
        public void RemoveItem(Ingredient ingredient)
        {
            foreach(KeyValuePair<Ingredient, KeyValuePair<AMOUNT_TYPE, double>> p in Items)
            {
                if (p.Key == ingredient)
                    Items.Remove(p.Key);
            }

            Refresh();
        }

        public void RemoveItem(string ingredient)
        {
            Ingredient i = IngredientHelper.GetIngredient(ingredient);
            if (i == null)
                return;

            RemoveItem(i);
        }

        public void ReduceAmountOfIngredient(Ingredient ingredient, double amount)
        {
            foreach (KeyValuePair<Ingredient, KeyValuePair<AMOUNT_TYPE, double>> p in Items)
            {
                if (p.Key == ingredient)
                {
                    double newValue = p.Value.Value - amount;
                    AMOUNT_TYPE type = p.Value.Key;

                    RemoveItem(p.Key);

                    if (newValue > 0.00) 
                        Items.Add(ingredient, new KeyValuePair<AMOUNT_TYPE, double>(type, newValue));

                    Refresh();
                    return;
                }                    
            }
            // TODO: not found
        }

        public void ReduceAmountOfIngredient(string ingredient, double amount)
        {
            Ingredient i = IngredientHelper.GetIngredient(ingredient);
            if (i == null)
                return;

            ReduceAmountOfIngredient(i, amount);
        }

        #endregion

        private void Refresh()
        {
            // TODO Refresh DB
            // TODO Refresh List
        }
    }
}
