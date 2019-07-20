using Microsoft.EntityFrameworkCore;

namespace Kiki.Models
{
    public class KikiContext : DbContext
    {
        public KikiContext(DbContextOptions<KikiContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BookAuthor>().HasKey(a => new {a.AuthorId, a.BookId});
            modelBuilder.Entity<SeriesAuthor>().HasKey(a => new {a.AuthorId, a.SeriesId});
        }
    }
}