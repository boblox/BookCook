using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataLayer.Interfaces
{
    public interface ICookBookContext
    {
        DbSet<Recipe> Recipes { get; set; }
        DbSet<RecipeRevision> RecipeRevisions { get; set; }
        int SaveChanges();
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
