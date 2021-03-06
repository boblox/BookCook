﻿using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Abstractions
{
    public interface IRecipeManager
    {
        IList<Recipe> GetRecipes();

        Recipe GetRecipe(int id);

        IList<RecipeRevision> GetRecipeRevisions(int recipeId);

        void CreateRecipe(Recipe recipe);

        void UpdateRecipe(Recipe recipe);

        bool DeleteRecipe(int recipeId);
    }
}
