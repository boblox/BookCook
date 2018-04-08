using API.Abstractions;
using API.Models;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DataModels = DataLayer.Models;

namespace API.Managers
{
    public class RecipeManager : IRecipeManager
    {
        private readonly CookBookContext _context;

        public RecipeManager(CookBookContext context)
        {
            _context = context;
        }

        private DataModels.RecipeRevision GetCurrentRecipeRevision(ICollection<DataModels.RecipeRevision> revisions)
        {
            return revisions.FirstOrDefault(i => i.EndDate == null);
        }

        private RecipeData GetRecipeData(DataModels.RecipeRevision recipeRevision)
        {
            return recipeRevision == null ?
                null :
                new RecipeData()
                {
                    Name = recipeRevision.Name,
                    Description = recipeRevision.Description
                };
        }

        public void DeleteRecipe(int recipeId)
        {
            throw new NotImplementedException();
        }

        public IList<RecipeRevision> GetRecipeRevisions(int recipeId)
        {
            throw new NotImplementedException();
        }

        public IList<Recipe> GetRecipes()
        {
            return _context.Recipes
                .Select(i => new Recipe()
                {
                    Id = i.Id,
                    DateCreated = i.DateCreated,
                    Deleted = i.Deleted,
                    Data = GetRecipeData(GetCurrentRecipeRevision(i.Revisions))
                })
                .ToList();
        }

        public void SaveRecipe(Recipe recipe)
        {
            throw new NotImplementedException();
        }
    }
}
