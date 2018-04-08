using System.Collections.Generic;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Abstractions
{
    public interface IRecipesController
    {
        IEnumerable<RecipeRevision> GetRecipeRevisions(int id);
        IEnumerable<Recipe> GetRecipes();
        void Delete(int id);
        void Post([FromBody] string value);
        void Put(int id, [FromBody] string value);
    }
}