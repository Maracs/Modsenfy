using Microsoft.EntityFrameworkCore;
namespace Modsenfy.DataAccessLayer.Data;
public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options) { }

    }
