using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class CookBookContext : DbContext, ICookBookContext
    {
        public CookBookContext(DbContextOptions<CookBookContext> options) : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeRevision> RecipeRevisions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>().ToTable("Recipe");
            modelBuilder.Entity<RecipeRevision>().ToTable("RecipeRevision");
        }
    }
}
