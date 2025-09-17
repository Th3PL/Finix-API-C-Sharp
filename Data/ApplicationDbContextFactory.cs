using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseOracle("User Id=RM550366;Password=280105;Data Source=oracle.fiap.com.br:1521/ORCL;");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
