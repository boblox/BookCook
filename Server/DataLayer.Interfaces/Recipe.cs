using System;
using System.Collections.Generic;

namespace DataLayer.Interfaces
{
    public class Recipe
    {
        public int Id { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        public bool Deleted { get; set; }

        public ICollection<RecipeRevision> Revisions { get; set; }
    }
}
