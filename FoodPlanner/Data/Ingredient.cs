using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodPlanner.Enums;
using FoodPlanner.Data;
using FoodPlanner.Helper;

namespace FoodPlanner.Data
{
    public class Ingredient : IDisposable
    {
        private Guid guid;
        private string name;
        private TYPE type;

        public Guid Guid { get { return guid; } }
        public string Name 
        { 
            get { return name; } 
            set 
            {
                // check, that ingredient is not a duplicate
                name = value;
                Data.DB.DBHandler.UpdateSingleValue("ingredients", "name", value, this.guid.ToString());
            } 
        }
        public TYPE Type 
        { 
            get { return type; }
            set
            {
                type = value;
                Data.DB.DBHandler.UpdateSingleValue("ingredients", "type", value.ToString(), this.guid.ToString());
            }
        }

        public Ingredient(string name, TYPE type, Guid guid = new Guid(), bool newItem = true)
        {
            this.name = name;
            this.type = type;
            this.guid = newItem ? Guid.NewGuid() : guid;

            if (newItem)
            {
                if (!DataHandling.CheckForIngredientExistance(name))
                {
                    // TODO: Create new ingredient (DB)
                }

                // TODO: Error Ingredient already existing
                this.Dispose();
            }
        }

        public void DeleteIngredient()
        {
            // TODO: Delete db relations
            // TODO Delete row in ingredients column

            this.Dispose();
        }

        public void Dispose()
        {
            // TODO Refresh ingredients list
        }
    }
}
