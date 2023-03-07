namespace MedokStore.Persistence
{
    public class DbInitializer
    {
        public static void Initializer(MedokStoreDbContext context)
        {
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
