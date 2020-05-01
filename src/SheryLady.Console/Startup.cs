namespace SheryLady.Console
{
    using Data;

    using Microsoft.EntityFrameworkCore;

    public class Startup
    {
        public static void Main()
        {
            using var db = new ApplicationDbContext();

            db.Database.Migrate();
        }
    }
}
