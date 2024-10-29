using KPZ_lab5.Models;
using Microsoft.EntityFrameworkCore;

namespace KPZ_lab5.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<ContributorHistory> ContributorHistories { get; set; }
        public DbSet<PrintingHouseBook> PrintingHouseBooks { get; set; }
        public DbSet<TextBook> TextBooks { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<PrintingHouse> PrintingHouses { get; set; }
        public DbSet<Text> Texts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite keys
            modelBuilder.Entity<PrintingHouseBook>()
                .HasKey(phb => new { phb.BookId, phb.PrintingHouseId });

            modelBuilder.Entity<TextBook>()
                .HasKey(tb => new { tb.BookId, tb.TextId });

            // Define relationships and other constraints
            modelBuilder.Entity<ContributorHistory>()
                .HasOne(ch => ch.Book)
                .WithMany(b => b.ContributorHistories)
                .HasForeignKey(ch => ch.BookId);

            modelBuilder.Entity<ContributorHistory>()
                .HasOne(ch => ch.Contributor)
                .WithMany(tm => tm.ContributorHistories)
                .HasForeignKey(ch => ch.ContributorId);

            modelBuilder.Entity<PrintingHouseBook>()
                .HasOne(phb => phb.Book)
                .WithMany(b => b.PrintingHouseBooks)
                .HasForeignKey(phb => phb.BookId);

            modelBuilder.Entity<PrintingHouseBook>()
                .HasOne(phb => phb.PrintingHouse)
                .WithMany(ph => ph.PrintingHouseBooks)
                .HasForeignKey(phb => phb.PrintingHouseId);

            modelBuilder.Entity<TextBook>()
                .HasOne(tb => tb.Book)
                .WithMany(b => b.TextBooks)
                .HasForeignKey(tb => tb.BookId);

            modelBuilder.Entity<TextBook>()
                .HasOne(tb => tb.Text)
                .WithMany(t => t.TextBooks)
                .HasForeignKey(tb => tb.TextId);
        }
    }
}
