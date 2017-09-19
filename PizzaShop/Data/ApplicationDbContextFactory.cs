using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PizzaShop.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer("Server=tcp:pizzashop20170913065207dbserver.database.windows.net,1433;Initial Catalog=PizzaShop20170913065207_db;Persist Security Info=False;User ID=isabellaalstrom@pizzashop20170913065207dbserver;Password={password};MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            return new ApplicationDbContext(builder.Options);
        }
    }
}
