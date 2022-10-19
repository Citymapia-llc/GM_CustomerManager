using GM.Store.Shared;
using Microsoft.EntityFrameworkCore;

namespace GM.Store.Client.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> Opts):base(Opts)
        {

        }
        public DbSet<Application> Application { get; set; } = null!;
    }
}
