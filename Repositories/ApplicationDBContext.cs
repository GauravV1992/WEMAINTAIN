using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Repositories
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext()
        {

        }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        public DbSet<DBCategory> Categories { get; set; }

    }
}

