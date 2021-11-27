using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodPlanner.Helper;
using FoodPlanner.Data;
using FoodPlanner.Enums;

namespace FoodPlanner.Data
{
    public class Recipe : IDisposable
    {
        private Guid guid = Guid.Empty;
        private Uri link = null;
        private string estimatedTime = null;
        private Dictionary<Ingredient, KeyValuePair<AMOUNT_TYPE, double>> ingredients = null;
        private List<string> steps = null;

        public Guid Guid { get { return guid; } }
        public Uri Link 
        {
            get { return link; }
            set
            {
                if (processProvidedUri(value))
                    Data.DB.DBHandler.UpdateSingleValue("recipes", "link", value.AbsoluteUri, this.guid.ToString());        // TODO Check if this is the right uri parameter
            }
        }
        public string EstimatedTime 
        { 
            get { return estimatedTime; }
            set
            {
                estimatedTime = value;
                Data.DB.DBHandler.UpdateSingleValue("recipes", "estimated_time", value, this.guid.ToString());
            }
        }
        public Dictionary<Ingredient, KeyValuePair<AMOUNT_TYPE, double>> Ingredients { get { return ingredients; } }
        public List<string> Steps 
        {
            get { return steps; }
            set
            {
                steps = value;
                Data.DB.DBHandler.UpdateSingleValue("recipes", "steps", value.ToString(), this.guid.ToString());            // TODO: make own table for steps 
            }
        }

        #region BUILD_RECIPE
        public Recipe(string link = null, Dictionary<Ingredient, KeyValuePair<AMOUNT_TYPE, double>> ingredients = null, string estimatedTime = null, List<string> steps = null, 
            Guid guid = new Guid(), bool newItem = true)
        {
            if(link != null)
                if(!processProvidedUri(new Uri(link)))
                    this.Dispose();

            processRemainingData(ingredients, estimatedTime, steps, guid, newItem);            
        }

        public Recipe(Uri link = null, Dictionary<Ingredient, KeyValuePair<AMOUNT_TYPE, double>> ingredients = null, string estimatedTime = null, List<string> steps = null, 
            Guid guid = new Guid(),  bool newItem = true)
        {
            if (link != null)
                if (!processProvidedUri(link))
                    this.Dispose();

            processRemainingData(ingredients, estimatedTime, steps, guid, newItem);            
        }

        private void processRemainingData(Dictionary<Ingredient, KeyValuePair<AMOUNT_TYPE, double>> ingredients, string estimatedTime, List<string> steps, Guid guid, bool newItem)
        {
            this.ingredients = ingredients;
            this.estimatedTime = estimatedTime;
            this.steps = steps;
            this.guid = newItem ? Guid.NewGuid() : guid;

            if (newItem)
            {
                // Create in db and link ingredients to it
            }
        }

        private bool processProvidedUri(Uri uri)
        {
            if (!HTTPClient.CheckLink(uri))
                return false;

            this.link = uri;
            return true;
        }

        #endregion

        #region INGREDIENT_HANDLING

        public void AddIngredient(Ingredient ingredient, double amount, AMOUNT_TYPE type)
        {
            if (DataHandling.CheckForIngredientExistance(ingredient))
            {
                if(amount > 0.00)
                {
                    // TODO Add ingredient to DB
                    ingredients.Add(ingredient, new KeyValuePair<AMOUNT_TYPE, double>(type, amount));
                    return;
                }
                // TODO Error Invalid amount
            }
            // TODO Ingredient not found
        } 

        public void RemoveIngredient(Ingredient ingredient)
        {
            foreach(Ingredient i in ingredients.Keys)
            {
                if (i.Name == ingredient.Name)
                {
                    // TODO: Remove form db 
                    ingredients.Remove(i);
                    return;
                }
            }
            // TODO: Ingredient not found error
        }

        public void AlterAmountOfIngredient(Ingredient ingredient, double amount, ALTER_TYPE type)
        {
            double newValue = 0.00;
            Ingredient toBeDReplaced = null;
            AMOUNT_TYPE amountType = AMOUNT_TYPE.GRAMM;

            foreach (KeyValuePair<Ingredient, KeyValuePair<AMOUNT_TYPE, double>> i in this.ingredients)
            {
                if(i.Key.Name == ingredient.Name)
                {
                    toBeDReplaced = i.Key;
                    amountType = i.Value.Key;
                    switch (type)
                    {
                        case ALTER_TYPE.ADD:
                            newValue = i.Value.Value + amount;
                            break;
                        case ALTER_TYPE.SUB:
                            newValue = i.Value.Value - amount;
                            break;
                        default:
                            newValue = amount;
                            break;
                    }
                    break;
                }                
            }

            if(toBeDReplaced == null)
            {
                // TODO: Ingredient not found error
                return;
            }

            if(newValue <= 0)
            {
                // Show error value must be bigger than null
                return;
            }

            this.ingredients.Remove(toBeDReplaced);
            this.ingredients.Add(ingredient, new KeyValuePair<AMOUNT_TYPE, double>(amountType, newValue));
        }

        #endregion

        public void Delete()
        {
            // Delete from database
            this.Dispose();
        }

        public void Dispose()
        {
            // TODO: Implement
        }
    }
}
