using System;
using System.Collections.Generic;
using System.Linq;
using API.Abstractions;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class RecipesController : Controller, IRecipesController
    {
        private readonly IRecipeManager _recipeManager;
        private readonly ILogger<IRecipesController> _logger;

        public RecipesController(IRecipeManager recipeManager, ILogger<IRecipesController> logger)
        {
            _recipeManager = recipeManager;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Recipe> GetRecipes()
        {
            return _recipeManager.GetRecipes();
        }

        [HttpGet("{id}/history")]
        public IEnumerable<RecipeRevision> GetRecipeRevisions(int id)
        {
            return _recipeManager.GetRecipeRevisions(id);
        }

        [HttpGet("{id}")]
        public Recipe GetRecipe(int id)
        {
            return _recipeManager.GetRecipe(id);
        }
        
        [HttpPost]
        public Recipe SaveRecipe([FromBody]Recipe recipe)
        {
            var recipeId = _recipeManager.SaveRecipe(recipe);
            return _recipeManager.GetRecipe(recipeId);
        }

        // PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        [HttpDelete("{id}")]
        public bool DeleteRecipe(int id)
        {
            return _recipeManager.DeleteRecipe(id);
        }
    }
}
