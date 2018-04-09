using System.Collections.Generic;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Abstractions
{
    public interface IRecipesController
    {
        IActionResult GetRecipes();
        IActionResult GetRecipeRevisions(int id);
        IActionResult GetRecipe(int id);
        IActionResult DeleteRecipe(int id);
        IActionResult SaveRecipe([FromBody] Recipe value);
        //void Put(int id, [FromBody] string value);
    }
}