using Microsoft.EntityFrameworkCore;
using PloomesTest.Models;

namespace PloomesTest.Data
{
    public class CustomerDb : DbContext
    {
        public CustomerDb(DbContextOptions<CustomerDb> options) : base(options) 
        {
        }

        public DbSet<Customer> Customers { get; set; } = null!;
    }

}