using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodPlanner.Helper;
using FoodPlanner.Enums;

namespace FoodPlanner.Data
{
    public class Food : IDisposable
    {
        private Guid guid;
        private string name;
        private Uri imageLink;
        private Recipe recipe;
        private byte score; 

        public Guid Guid { get; }
        public string Name { get; }        
        public Uri ImageLink { get; }
        public Recipe Recipe { get; }
        public byte Score { get; }           // value between 1-10. Higher number means, that the person likes this food more. set {  }

        public Food(string name, Recipe recipe, Uri imageLink = null, byte score = 5, Guid guid = new Guid(), bool newItem = true)
        {
            this.name = name;
            this.recipe = recipe;
            this.imageLink = imageLink;
            this.guid = newItem ? Guid.NewGuid() : guid;

            if (newItem)
            {      
                // Create food in db and link recipe to it
            }        
        }

        public void DeleteFood()
        {
            // TODO: Delete db entry (guid) and remove connection to recipe
            this.Dispose();
        }

        public void SetFoodScore(byte value)
        {
            this.score = (value > 0 && value <= 10) ? value : this.score;
            Data.DB.DBHandler.UpdateSingleValue("Food", "food_score", this.score.ToString(), this.guid.ToString());
        }

        public void Dispose()
        {
            // TODO: Refresh food list 
        }

    }
}
