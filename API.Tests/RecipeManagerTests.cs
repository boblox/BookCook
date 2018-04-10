using API.Managers;
using API.Tests.Helpers;
using DataLayer;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace API.Tests
{
    /// <summary>
    /// Class for testing RecipeManager
    /// </summary>
    [TestClass]
    public class RecipeManagerTests : TestsBase
    {
        private ILogger<RecipeManager> _logger;
        private DbContextOptions<CookBookContext> _options;
        private DataSupplier _dataSupplier;

        private RecipeManager GetManager(ICookBookContext context)
        {
            return new RecipeManager(context, _logger);
        }

        private void UseContext(Action<ICookBookContext> action)
        {
            using (var context = new CookBookContext(_options))
            {
                action(context);
            }
        }

        private void PrepareTestData_Set1()
        {
            UseContext(context =>
            {
                context.Recipes.AddRange(_dataSupplier.GetRecipes_Set1());
                context.SaveChanges();
            });
        }

        [TestInitialize]
        public void Setup()
        {
            _logger = Mock.Of<ILogger<RecipeManager>>();
            _options = new DbContextOptionsBuilder<CookBookContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _dataSupplier = new DataSupplier();
        }

        //[TestCleanup]
        //public void Cleanup()
        //{
        //    UseContext(context =>
        //    {
        //        context.Database.EnsureDeleted();
        //    });
        //}

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullConstructorArgument()
        {
            this.PrepareTestData_Set1();

            UseContext(context =>
            {
                var manager = GetManager(null);
            });
        }

        [TestMethod]
        public void GetRecipe_Ok()
        {
            this.PrepareTestData_Set1();

            UseContext(context =>
            {
                var manager = GetManager(context);

                var recipe = manager.GetRecipe(1);

                Assert.IsNotNull(recipe);
                Assert.IsNotNull(recipe.Data);
                Assert.AreEqual("First recipe. Revision 2", recipe.Data.Name);
                Assert.AreEqual("First recipe description. Revision 2", recipe.Data.Description);
                Assert.AreEqual(new DateTime(2018, 1, 1), recipe.DateCreated);
                Assert.AreEqual(false, recipe.Deleted);
                Assert.AreEqual(1, recipe.Id);
            });
        }

        [TestMethod]
        public void GetRecipe_NullResult()
        {
            this.PrepareTestData_Set1();

            UseContext(context =>
            {
                var manager = GetManager(context);

                var recipe = manager.GetRecipe(10);

                Assert.IsNull(recipe);
            });
        }

        [TestMethod]
        public void GetRecipes_Ok()
        {
            this.PrepareTestData_Set1();

            UseContext(context =>
            {
                var manager = GetManager(context);

                var recipes = manager.GetRecipes();

                Assert.IsNotNull(recipes);
                Assert.AreEqual(2, recipes.Count);
                var recipe = recipes[0];
                Assert.AreEqual("First recipe. Revision 2", recipe.Data.Name);
                Assert.AreEqual("First recipe description. Revision 2", recipe.Data.Description);
                Assert.AreEqual(new DateTime(2018, 1, 1), recipe.DateCreated);
                Assert.AreEqual(false, recipe.Deleted);
                Assert.AreEqual(1, recipe.Id);
            });
        }

        [TestMethod]
        public void GetRecipeRevisions_Ok()
        {
            this.PrepareTestData_Set1();

            UseContext(context =>
            {
                var manager = GetManager(context);

                var recipeRevisions = manager.GetRecipeRevisions(1);

                Assert.IsNotNull(recipeRevisions);
                Assert.AreEqual(2, recipeRevisions.Count);
                var recipeRevision = recipeRevisions[0];
                Assert.AreEqual("First recipe", recipeRevision.Data.Name);
                Assert.AreEqual("First recipe description", recipeRevision.Data.Description);
                Assert.AreEqual(new DateTime(2018, 1, 1), recipeRevision.StartDate);
                Assert.AreEqual(new DateTime(2018, 3, 12), recipeRevision.EndDate);
                Assert.AreEqual(1, recipeRevision.Version);
                Assert.AreEqual(1, recipeRevision.Id);
            });
        }

        [TestMethod]
        public void GetRecipeRevisions_NullResult()
        {
            this.PrepareTestData_Set1();

            UseContext(context =>
            {
                var manager = GetManager(context);

                var recipeRevisions = manager.GetRecipeRevisions(10);

                Assert.IsNull(recipeRevisions);
            });
        }

        [TestMethod]
        public void AddRecipe_Ok()
        {
            //this.PrepareTestData_Set1();

            UseContext(context =>
            {
                var manager = GetManager(context);

                var recipeToAdd = _dataSupplier.GetRecipeForAdding_Ok();
                manager.CreateRecipe(recipeToAdd);
                manager.CreateRecipe(recipeToAdd);

                var recipe = manager.GetRecipe(recipeToAdd.Id);
                var recipes = manager.GetRecipes();

                Assert.IsNotNull(recipe);
                Assert.IsNotNull(recipe.Data);
                Assert.AreEqual(recipes.Count, 2);
                Assert.AreEqual(recipe.Data.Name, "Added recipe");
                Assert.AreEqual(recipe.Data.Description, "Added recipe description");
                Assert.AreEqual(recipe.DateCreated.Date, DateTime.UtcNow.Date);
                Assert.AreEqual(recipe.Deleted, false);
                Assert.AreEqual(recipe.Id, recipeToAdd.Id);
            });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddRecipe_NullArgument()
        {
            //this.PrepareTestData_Set1();

            UseContext(context =>
            {
                var manager = GetManager(context);

                var recipeToAdd = _dataSupplier.GetRecipeForAdding_Ok();
                recipeToAdd.Data = null;
                manager.CreateRecipe(recipeToAdd);
            });
        }

        [TestMethod]
        public void DeleteRecipe_Ok()
        {
            //this.PrepareTestData_Set1();

            UseContext(context =>
            {
                var manager = GetManager(context);

                var firstRecipe = _dataSupplier.GetRecipeForAdding_Ok();
                manager.CreateRecipe(firstRecipe);
                var secondRecipe = _dataSupplier.GetRecipeForAdding_Ok();
                manager.CreateRecipe(secondRecipe);

                var result = manager.DeleteRecipe(secondRecipe.Id);

                var recipe = manager.GetRecipe(firstRecipe.Id);
                var deletedRecipe = manager.GetRecipe(secondRecipe.Id);
                var recipes = manager.GetRecipes();

                Assert.IsNull(deletedRecipe);
                Assert.IsTrue(result);
                Assert.IsNotNull(recipe);
                Assert.IsNotNull(recipe.Data);
                Assert.AreEqual(recipes.Count, 1);
                Assert.AreEqual(recipe.Data.Name, "Added recipe");
                Assert.AreEqual(recipe.Data.Description, "Added recipe description");
                Assert.AreEqual(recipe.DateCreated.Date, DateTime.UtcNow.Date);
                Assert.AreEqual(recipe.Deleted, false);
                Assert.AreEqual(recipe.Id, firstRecipe.Id);
            });
        }

        [TestMethod]
        public void DeleteRecipe_Fail()
        {
            //this.PrepareTestData_Set1();

            UseContext(context =>
            {
                var manager = GetManager(context);

                var firstRecipe = _dataSupplier.GetRecipeForAdding_Ok();
                manager.CreateRecipe(firstRecipe);
                var secondRecipe = _dataSupplier.GetRecipeForAdding_Ok();
                manager.CreateRecipe(secondRecipe);

                var result = manager.DeleteRecipe(50);

                var recipes = manager.GetRecipes();

                Assert.IsFalse(result);
                Assert.AreEqual(recipes.Count, 2);
            });
        }

        [TestMethod]
        public void UpdateRecipe_Ok()
        {
            //this.PrepareTestData_Set1();

            UseContext(context =>
            {
                var manager = GetManager(context);

                var recipeToAdd = _dataSupplier.GetRecipeForAdding_Ok();
                manager.CreateRecipe(recipeToAdd);
                recipeToAdd.Data.Name = "New name";
                recipeToAdd.Data.Description = "New description";
                manager.UpdateRecipe(recipeToAdd);

                var recipe = manager.GetRecipe(recipeToAdd.Id);
                var recipes = manager.GetRecipes();

                Assert.IsNotNull(recipe);
                Assert.IsNotNull(recipe.Data);
                Assert.AreEqual(recipes.Count, 1);
                Assert.AreEqual(recipe.Data.Name, "New name");
                Assert.AreEqual(recipe.Data.Description, "New description");
                Assert.AreEqual(recipe.DateCreated.Date, DateTime.UtcNow.Date);
                Assert.AreEqual(recipe.Deleted, false);
                Assert.AreEqual(recipe.Id, recipeToAdd.Id);
            });
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void UpdateRecipe_NullRecipeRevisions()
        {
            //this.PrepareTestData_Set1();

            UseContext(context =>
            {
                var manager = GetManager(context);

                var recipeToAdd = _dataSupplier.GetRecipeForAdding_Ok();
                manager.CreateRecipe(recipeToAdd);
                recipeToAdd.Id = 100;
                recipeToAdd.Data.Description = "New description";
                manager.UpdateRecipe(recipeToAdd);
            });
        }
    }
}
