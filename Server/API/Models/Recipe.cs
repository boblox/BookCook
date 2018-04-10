using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        public RecipeData Data { get; set; }

        public bool Deleted { get; set; }

        //public IList<RecipeRevision> Revisions { get; set; }
    }
}
