using Microsoft.EntityFrameworkCore;
using ConnectionLibrary.Entities;

namespace ConnectionLibrary.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Person> People { get; set; } // people is a list of type Person from DB
    }
}