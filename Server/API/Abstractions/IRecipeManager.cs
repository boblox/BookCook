using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Abstractions
{
    public interface IRecipeManager
    {
        IList<Recipe> GetRecipes();

        IList<RecipeRevision> GetRecipeRevisions(int recipeId);

        void SaveRecipe(Recipe recipe);

        void DeleteRecipe(int recipeId);
    }
}
