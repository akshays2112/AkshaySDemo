using AkshaySDemoModels.PostgreSQL;
using System.Collections.Generic;
using System.Linq;

namespace AkshaySDemoPostgreSQL.Data
{
    public class AkshaySDemoPostgreSQLDbOperations
    {
        private readonly AkshaySDemoPostgreSQLContext rc = new AkshaySDemoPostgreSQLContext();

        public IEnumerable<Recipe> GetAllRecipes()
        {
            try
            {
               return rc.Recipes.ToList();
            }
            catch { throw; }
        }
        //To Add new recipe
        public void AddRecipe(Recipe recipe)
        {
            try
            {
                rc.Recipes.Add(recipe);
                rc.SaveChanges();
            }
            catch { throw; }
        }
        //To Update particular recipe
        public void UpdateRecipe(Recipe recipe)
        {
            try
            {
                rc.Update(recipe);
                rc.SaveChanges();
            }
            catch { throw; }
        }
        //Get the particular recipe
        public Recipe GetRecipe(int id)
        {
            try
            {
                Recipe recipe = rc.Recipes.Find(id);
                return recipe;
            }
            catch
            {
                throw;
            }
        }
        //To Delete particular recipe
        public void DeleteRecipe(int id)
        {
            try
            {
                Recipe recipe = rc.Recipes.Find(id);
                rc.Remove(recipe);
                rc.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    }
}
