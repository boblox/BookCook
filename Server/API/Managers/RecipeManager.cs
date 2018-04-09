using API.Abstractions;
using API.Models;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;

using DataModels = DataLayer.Models;
using Microsoft.Extensions.Logging;

namespace API.Managers
{
    public class RecipeManager : IRecipeManager
    {
        private readonly CookBookContext _context;
        private readonly ILogger<IRecipesController> _logger;

        public RecipeManager(CookBookContext context, ILogger<IRecipesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        private DataModels.Recipe GetDbRecipe(int id)
        {
            return _context.Recipes
                .Where(i => i.Id == id && !i.Deleted)
                .FirstOrDefault();
        }

        private DataModels.RecipeRevision GetRecipeCurrentRevision(ICollection<DataModels.RecipeRevision> revisions)
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

        public bool DeleteRecipe(int recipeId)
        {
            var recipe = GetDbRecipe(recipeId);
            if (recipe != null)
            {
                recipe.Deleted = true;
                _context.Update(recipe);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public IList<RecipeRevision> GetRecipeRevisions(int recipeId)
        {
            var dbRecipe = GetDbRecipe(recipeId);
            _logger.LogInformation((dbRecipe == null).ToString());
            _logger.LogWarning(recipeId.ToString());
            if (dbRecipe != null)
            {
                _context.Entry(dbRecipe).Collection(i => i.Revisions).Load();
                return dbRecipe.Revisions
                    //.Where(i => i.EndDate != null)
                    .Select(i => new RecipeRevision()
                    {
                        StartDate = i.StartDate,
                        EndDate = i.EndDate,
                        Id = i.Id,
                        Version = i.Version,
                        Data = GetRecipeData(i)
                    })
                    .ToList();
            }
            //TODO: maybe throw exception?
            return null;
        }

        public IList<Recipe> GetRecipes()
        {
            return _context.Recipes
                .Where(i => !i.Deleted)
                .Select(i => new Recipe()
                {
                    //TODO: add auto-mapper
                    Id = i.Id,
                    DateCreated = i.DateCreated,
                    Deleted = i.Deleted,
                    Data = GetRecipeData(GetRecipeCurrentRevision(i.Revisions))
                })
                .ToList();
        }

        public Recipe GetRecipe(int id)
        {
            var dbRecipe = GetDbRecipe(id);
            if (dbRecipe != null)
            {
                _context.Entry(dbRecipe).Collection(i => i.Revisions).Load();
                return new Recipe()
                {
                    //TODO: add auto-mapper
                    Id = dbRecipe.Id,
                    DateCreated = dbRecipe.DateCreated,
                    Deleted = dbRecipe.Deleted,
                    Data = GetRecipeData(GetRecipeCurrentRevision(dbRecipe.Revisions))
                };
            }
            //TODO: maybe throw exception?
            return null;
        }

        public int SaveRecipe(Recipe recipe)
        {
            var recipeId = recipe.Id;
            var dbRecipe = new DataModels.Recipe()
            {
                //Id = recipe.Id,
                DateCreated = DateTime.Now,
            };
            var dbRecipeRevision = new DataModels.RecipeRevision()
            {
                Name = recipe.Data.Name,
                Description = recipe.Data.Description,
                StartDate = DateTime.Now,
                EndDate = null,
                Version = 1,
            };

            if (recipeId == 0)
            {
                this._context.Recipes.Add(dbRecipe);
                this._context.SaveChanges();
                dbRecipeRevision.Recipe = dbRecipe;
                recipeId = dbRecipe.Id;
                this._context.RecipeRevisions.Add(dbRecipeRevision);
                this._context.SaveChanges();
            }
            else
            {
                var revisions = this._context.RecipeRevisions
                    .Where(i => i.RecipeId == recipeId).ToList();
                var latestRevision = GetRecipeCurrentRevision(revisions);
                latestRevision.EndDate = DateTime.Now;
                dbRecipeRevision.Version = latestRevision.Version + 1;
                dbRecipeRevision.RecipeId = recipeId;
                this._context.RecipeRevisions.Update(latestRevision); //Update latest revision - from now - old
                this._context.RecipeRevisions.Add(dbRecipeRevision); //Add new revision
                this._context.SaveChanges();
            }

            return recipeId;
        }
    }
}
