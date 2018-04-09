using System.Collections.Generic;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Abstractions
{
    public interface IRecipesController
    {
        IEnumerable<Recipe> GetRecipes();
        IEnumerable<RecipeRevision> GetRecipeRevisions(int id);
        Recipe GetRecipe(int id);
        bool DeleteRecipe(int id);
        Recipe SaveRecipe([FromBody] Recipe value);
        //void Put(int id, [FromBody] string value);
    }
}