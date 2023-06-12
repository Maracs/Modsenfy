using Microsoft.EntityFrameworkCore;

namespace Modsenfy.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options) { }

    }
}
