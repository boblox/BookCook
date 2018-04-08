using System;
using System.Collections.Generic;
using System.Linq;
using API.Abstractions;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class RecipesController : Controller, IRecipesController
    {
        private readonly IRecipeManager _recipeManager;

        public RecipesController(IRecipeManager recipeManager)
        {
            _recipeManager = recipeManager;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Recipe> GetRecipes()
        {
            return _recipeManager.GetRecipes();

            //return new List<Recipe>() {
            //    new Recipe() {
            //        Description = "First Recipe"
            //    },
            //    new Recipe() {
            //        Description = "Second Recipe"
            //    }
            //};
        }

        // GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        [HttpGet("{id}/history")]
        public IEnumerable<RecipeRevision> GetRecipeRevisions(int id)
        {
            return null;
            //return new List<Recipe>() {
            //    new Recipe() {
            //        Description = "First History Recipe"
            //    },
            //    new Recipe() {
            //        Description = "Second History Recipe"
            //    }
            //};
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
