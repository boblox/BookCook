using System;

namespace DataLayer.Interfaces
{
    public class RecipeRevision
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Version { get; set; }

        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; }

        public DateTimeOffset? StartDate { get; set; }

        public DateTimeOffset? EndDate { get; set; }
    }
}
