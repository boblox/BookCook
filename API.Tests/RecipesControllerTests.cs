using API.Abstractions;
using API.Controllers;
using API.Models;
using API.Tests.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Tests
{
    [TestClass]
    public class RecipesControllerTests : TestsBase
    {
        private Mock<IRecipeManager> _mockRecipeManager;
        private Mock<ILogger<IRecipesController>> _mockLogger;
        private DataSupplier _dataSupplier;
        private IRecipesController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockRecipeManager = new Mock<IRecipeManager>();
            _mockLogger = new Mock<ILogger<IRecipesController>>();
            _dataSupplier = new DataSupplier();
            _controller = new RecipesController(_mockRecipeManager.Object, _mockLogger.Object);
        }

        [TestMethod]
        public void GetRecipe()
        {
            var recipes = _dataSupplier.RecipeManager_GetRecipes();
            _mockRecipeManager.Setup(i => i.GetRecipe(1)).Returns(recipes[0]);
            _mockRecipeManager.Setup(i => i.GetRecipe(2)).Returns(recipes[1]);
            _mockRecipeManager.Setup(i => i.GetRecipe(3)).Returns<Recipe>(null);

            var actionResult1 = _controller.GetRecipe(1) as OkObjectResult;
            var result1 = actionResult1.Value as Recipe;
            var actionResult2 = _controller.GetRecipe(3) as NotFoundResult;

            Assert.IsNotNull(actionResult1);
            Assert.IsNotNull(actionResult1.Value);
            Assert.AreEqual(1, result1.Id);
            Assert.AreEqual("Recipe 1", result1.Data.Name);
            Assert.AreEqual("Recipe 1 description", result1.Data.Description);
            Assert.AreEqual(new DateTime(2018, 1, 12), result1.DateCreated);

            Assert.IsNotNull(actionResult2);
        }

        [TestMethod]
        public void GetRecipes()
        {
            var recipes = _dataSupplier.RecipeManager_GetRecipes();
            _mockRecipeManager.Setup(i => i.GetRecipes()).Returns(recipes);

            var actionResult1 = _controller.GetRecipes() as OkObjectResult;
            var resultList = actionResult1.Value as List<Recipe>;
            var result = resultList[0];

            Assert.IsNotNull(actionResult1);
            Assert.IsNotNull(actionResult1.Value);
            Assert.AreEqual(2, resultList.Count);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Recipe 1", result.Data.Name);
            Assert.AreEqual("Recipe 1 description", result.Data.Description);
            Assert.AreEqual(new DateTime(2018, 1, 12), result.DateCreated);
        }
    }
}
