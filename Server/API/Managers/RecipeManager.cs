using API.Abstractions;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using IDataLayer = DataLayer.Interfaces;
using Microsoft.Extensions.Logging;


namespace API.Managers
{
    public class RecipeManager : IRecipeManager
    {
        private readonly IDataLayer.ICookBookContext _context;
        private readonly ILogger<IRecipesController> _logger;

        public RecipeManager(IDataLayer.ICookBookContext context, ILogger<IRecipesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        private IDataLayer.Recipe GetDbRecipe(int id)
        {
            return _context.Recipes
                .Where(i => i.Id == id && !i.Deleted)
                .FirstOrDefault();
        }

        private IDataLayer.RecipeRevision GetRecipeCurrentRevision(ICollection<IDataLayer.RecipeRevision> revisions)
        {
            return revisions.FirstOrDefault(i => i.EndDate == null);
        }

        private RecipeData GetRecipeData(IDataLayer.RecipeRevision recipeRevision)
        {
            return recipeRevision == null ?
                null :
                new RecipeData()
                {
                    Name = recipeRevision.Name,
                    Description = recipeRevision.Description
                };
        }

        public IList<RecipeRevision> GetRecipeRevisions(int recipeId)
        {
            var dbRecipe = GetDbRecipe(recipeId);
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
            return null;
        }

        public void CreateRecipe(Recipe recipe)
        {
            if (recipe == null || recipe.Data == null) throw new ArgumentNullException();

            //TODO: bad behaviour: code depends on realization!
            var dbRecipe = new IDataLayer.Recipe()
            {
                DateCreated = DateTime.Now,
                Revisions = new List<IDataLayer.RecipeRevision>
                {
                    new IDataLayer.RecipeRevision()
                    {
                        Name = recipe.Data.Name,
                        Description = recipe.Data.Description,
                        StartDate = DateTime.Now,
                        EndDate = null,
                        Version = 1,
                    }
                }
            };

            this._context.Recipes.Add(dbRecipe);
            this._context.SaveChanges();
            recipe.Id = dbRecipe.Id;
        }

        public void UpdateRecipe(Recipe recipe)
        {
            if (recipe == null || recipe.Data == null || recipe.Id <= 0) throw new ArgumentNullException();

            var revisions = this._context.RecipeRevisions
                .Where(i => i.RecipeId == recipe.Id).ToList();
            if (revisions == null) throw new ArgumentNullException();

            var latestRevision = GetRecipeCurrentRevision(revisions);
            latestRevision.EndDate = DateTime.Now;
            var dbRecipeRevision = new IDataLayer.RecipeRevision()
            {
                Name = recipe.Data.Name,
                Description = recipe.Data.Description,
                StartDate = DateTime.Now,
                EndDate = null,
                Version = latestRevision.Version + 1,
                RecipeId = latestRevision.RecipeId,
            };

            this._context.RecipeRevisions.Update(latestRevision); //Update latest revision - from now - old
            this._context.RecipeRevisions.Add(dbRecipeRevision); //Add new revision
            this._context.SaveChanges();
        }

        public bool DeleteRecipe(int recipeId)
        {
            var recipe = GetDbRecipe(recipeId);
            if (recipe != null)
            {
                recipe.Deleted = true;
                _context.Recipes.Update(recipe);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
