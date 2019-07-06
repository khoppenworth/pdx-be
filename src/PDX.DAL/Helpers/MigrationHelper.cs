using PDX.Domain;

namespace PDX.DAL.Helpers
{
    public static class MigrationHelper
    {
        public static void Clear<T>(this Microsoft.EntityFrameworkCore.DbSet<T> dbSet) where T : BaseEntity
        {
            dbSet.RemoveRange(dbSet);
        }
    }
}