using backend_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace backend_api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Achievement> Achievement { get; set; }

        public ApplicationDbContext(
            DbContextOptions options) : base(options)
        {
        }
    }
}
