using HoteLove.Models;
using Microsoft.EntityFrameworkCore;

namespace HoteLove
{
    public class DbHoteLoveContext : DbContext
    {
        public DbHoteLoveContext(DbContextOptions<DbHoteLoveContext> options) : base(options) { }

        public DbSet<HotelModel> Hotels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
