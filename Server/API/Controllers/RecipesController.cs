using API.Abstractions;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

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
        public IActionResult GetRecipes()
        {
            return Ok(_recipeManager.GetRecipes());
        }

        [HttpGet("{id}/history")]
        public IActionResult GetRecipeRevisions(int id)
        {
            return Ok(_recipeManager.GetRecipeRevisions(id));
        }

        [HttpGet("{id}")]
        public IActionResult GetRecipe(int id)
        {
            var recipe = _recipeManager.GetRecipe(id);
            if (recipe != null)
            {
                return Ok(recipe);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult SaveRecipe([FromBody]Recipe recipe)
        {
            if (recipe == null || recipe.Data == null ||
                recipe.Id < 0 || string.IsNullOrEmpty(recipe.Data.Name))
            {
                return BadRequest();
            }

            if (recipe.Id == 0)
            {
                _recipeManager.CreateRecipe(recipe);
            }
            else
            {
                var dbRecipe = _recipeManager.GetRecipe(recipe.Id);
                if (dbRecipe == null)
                {
                    return NotFound();
                }
                _recipeManager.UpdateRecipe(recipe);
            }
            return Ok(_recipeManager.GetRecipe(recipe.Id));
        }

        // PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        [HttpDelete("{id}")]
        public IActionResult DeleteRecipe(int id)
        {
            var result = _recipeManager.DeleteRecipe(id);
            if (result) return Ok(result);
            return NotFound();
        }
    }
}
