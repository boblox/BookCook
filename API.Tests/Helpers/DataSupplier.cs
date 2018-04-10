using System;
using System.Collections.Generic;
using System.Text;
using IDataLayer = DataLayer.Interfaces;
using IDomainLayer = API.Models;

namespace API.Tests.Helpers
{
    public class DataSupplier
    {
        public IDomainLayer.Recipe GetRecipeForAdding_Ok()
        {
            return new IDomainLayer.Recipe
            {
                Data = new IDomainLayer.RecipeData
                {
                    Name = "Added recipe",
                    Description = "Added recipe description"
                }
            };
        }

        public List<IDataLayer.Recipe> GetRecipes_Set1()
        {
            return new List<IDataLayer.Recipe>
            {
                new IDataLayer.Recipe
                {
                    //Id = 1,
                    DateCreated = new DateTime(2018, 1, 1),
                    Deleted = false,
                    Revisions = new List<IDataLayer.RecipeRevision>
                    {
                        new IDataLayer.RecipeRevision
                        {
                            //Id = 1,
                            Name = "First recipe",
                            Description = "First recipe description",
                            StartDate = new DateTime(2018, 1, 1),
                            EndDate = new DateTime(2018, 3, 12),
                            Version = 1
                        },
                        new IDataLayer.RecipeRevision
                        {
                            //Id = 2,
                            Name = "First recipe. Revision 2",
                            Description = "First recipe description. Revision 2",
                            StartDate = new DateTime(2018, 3, 12),
                            EndDate = null,
                            Version = 2
                        }
                    }
                },
                new IDataLayer.Recipe
                {
                    //Id = 2,
                    DateCreated = new DateTime(2018, 2, 9),
                    Deleted = false,
                    Revisions = new List<IDataLayer.RecipeRevision>
                    {
                        new IDataLayer.RecipeRevision
                        {
                            //Id = 3,
                            Name = "Second recipe",
                            Description = "Second recipe",
                            StartDate = new DateTime(2018, 2, 9),
                            EndDate = null,
                            Version = 1
                        }
                    }
                },
                new IDataLayer.Recipe
                {
                    //Id = 3,
                    DateCreated = new DateTime(2018, 3, 20),
                    Deleted = true,
                    Revisions = new List<IDataLayer.RecipeRevision>
                    {
                        new IDataLayer.RecipeRevision
                        {
                            //Id = 4,
                            Name = "Third recipe (deleted)",
                            Description = "Thirt recipe (deleted)",
                            StartDate = new DateTime(2018, 3, 20),
                            EndDate = null,
                            Version = 1
                        }
                    }
                }
            };
        }

        public List<IDomainLayer.Recipe> RecipeManager_GetRecipes()
        {
            return new List<IDomainLayer.Recipe>
            {
                new IDomainLayer.Recipe
                {
                    Id = 1,
                    DateCreated = new DateTime(2018, 1, 12),
                    Deleted = false,
                    Data = new IDomainLayer.RecipeData
                    {
                        Name = "Recipe 1",
                        Description = "Recipe 1 description"
                    }
                },
                new IDomainLayer.Recipe
                {
                    Id = 2,
                    DateCreated = new DateTime(2018, 3, 25),
                    Deleted = false,
                    Data = new IDomainLayer.RecipeData
                    {
                        Name = "Recipe 2",
                        Description = "Recipe 2 description"
                    }
                }
            };
        }
    }
}
