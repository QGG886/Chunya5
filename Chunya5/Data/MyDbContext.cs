using Microsoft.EntityFrameworkCore;

namespace Chunya5.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options):base(options)
        {

        }
    }
}
