using Microsoft.EntityFrameworkCore;

namespace CustomerInfo.Data
{
    public class AppDbContext:DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoices> Invoices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=CustomersInfo;User Id=sas;Password=P@ssw0rd;");
        }
    }
}
