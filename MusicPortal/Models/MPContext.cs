using Microsoft.EntityFrameworkCore;

namespace MusicPortal.Models
{
    public class MPContext : DbContext
    {
        public MPContext(DbContextOptions<MPContext> options) : base(options) { }
        public DbSet<Song>? Songs { get; set; }
        public DbSet<Genre>? Genres { get; set; }
        public DbSet<User>? Users { get; set; }

       
    }
}
