using HoteLove.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HoteLove
{
    public class DbHoteLoveContext : IdentityDbContext
    {
        public DbHoteLoveContext(DbContextOptions<DbHoteLoveContext> options) : base(options) { }

        public DbSet<HotelModel> Hotels { get; set; }
        public DbSet<CommentModel> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
