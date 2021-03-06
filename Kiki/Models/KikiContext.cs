using System;
using Kiki.Models.Data;
using Kiki.Models.Identity;
using Kiki.Models.Metadata;
using Kiki.Models.Scanning;
using Kiki.Models.System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kiki.Models
{
    public class KikiContext : IdentityDbContext<KikiUser, KikiRole, Guid>
    {
        public KikiContext(DbContextOptions<KikiContext> options) : base(options) { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Data Source =kiki.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BookAuthor>().HasKey(a => new {a.AuthorId, a.BookId});
            modelBuilder.Entity<SeriesAuthor>().HasKey(a => new {a.AuthorId, a.SeriesId});
            modelBuilder.Entity<AudioFile>().HasKey(a => new {a.AudioBookId, a.TrackNumber});
            modelBuilder.Entity<AudioTags>()
                        .Property(e => e.AlbumArtists)
                        .HasConversion(
                                       v => string.Join(',', v),
                                       v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<AudioTags>()
                        .Property(e => e.Genres)
                        .HasConversion(
                                       v => string.Join(',', v),
                                       v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<AudioTags>()
                        .Property(e => e.Performers)
                        .HasConversion(
                                       v => string.Join(',', v),
                                       v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));
        }


        public DbSet<AudioBook>         AudioBooks       { get; set; }
        public DbSet<AudioFile>         AudioFiles       { get; set; }
        public DbSet<AudioTags>         AudioFileTags    { get; set; }
        public DbSet<AudioBookProgress> BookProgresses   { get; set; }
        public DbSet<Author>            Authors          { get; set; }
        public DbSet<BookAuthor>        BookAuthors      { get; set; }
        public DbSet<AudioFileProgress> FileProgresses   { get; set; }
        public DbSet<MediaDirectory>    MediaDirectories { get; set; }
        public DbSet<PlayHistoryEntry>  PlayHistory      { get; set; }
        public DbSet<Publisher>         Publishers       { get; set; }
        public DbSet<Series>            Series           { get; set; }
        public DbSet<SeriesAuthor>      SeriesAuthors    { get; set; }
        public DbSet<SeriesProgress>    SeriesProgresses { get; set; }
    }
}