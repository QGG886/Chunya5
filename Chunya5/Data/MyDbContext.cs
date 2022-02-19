using Chunya5.Models;
using Microsoft.EntityFrameworkCore;

namespace Chunya5.Data
{
    public class MyDbContext : DbContext
    {

        public MyDbContext(DbContextOptions<MyDbContext> options):base(options)
        {

        }

        public DbSet<Bonds> Bonds { get; set; }

        public DbSet<Trade> Trade { get; set; }

    }
}
