using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kiki.Models
{
    public class KikiContext : IdentityDbContext<KikiUser, KikiRole, Guid>
    {
        public KikiContext(DbContextOptions<KikiContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<KikiUser>(b => b.Property(u => u.Id).HasDefaultValueSql("newsequentialid()"));
            modelBuilder.Entity<KikiRole>(b => b.Property(u => u.Id).HasDefaultValueSql("newsequentialid()"));
            modelBuilder.Entity<BookAuthor>().HasKey(a => new {a.AuthorId, a.BookId});
            modelBuilder.Entity<SeriesAuthor>().HasKey(a => new {a.AuthorId, a.SeriesId});
            modelBuilder.Entity<AudioFile>().HasKey(a => new {a.AudioBookId, a.TrackNumber});
        }


        public DbSet<AudioBook>         AudioBooks       { get; set; }
        public DbSet<AudioFile>         AudioFiles       { get; set; }
        public DbSet<AudioBookProgress> BookProgresses   { get; set; }
        public DbSet<Author>            Authors          { get; set; }
        public DbSet<Book>              Books            { get; set; }
        public DbSet<BookAuthor>        BookAuthors      { get; set; }
        public DbSet<DebugLogEntry>     DebugLog         { get; set; }
        public DbSet<AudioFileProgress> FileProgresses   { get; set; }
        public DbSet<PlayHistoryEntry>  PlayHistory      { get; set; }
        public DbSet<Series>            Series           { get; set; }
        public DbSet<SeriesAuthor>      SeriesAuthors    { get; set; }
        public DbSet<SeriesProgress>    SeriesProgresses { get; set; }
    }
}