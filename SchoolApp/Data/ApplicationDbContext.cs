using SchoolApp.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Models;
using System.ComponentModel.DataAnnotations.Schema;


// PASUL 3 - useri si roluri

namespace SchoolApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Message> Messages{ get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // definire relatii cu modelele Bookmark si Article (FK)
            /*
            modelBuilder.Entity<ArticleBookmark>()
                .HasOne(ab => ab.Article)
                .WithMany(ab => ab.ArticleBookmarks)
                .HasForeignKey(ab => ab.ArticleId);

            modelBuilder.Entity<ArticleBookmark>()
                .HasOne(ab => ab.Bookmark)
                .WithMany(ab => ab.ArticleBookmarks)
                .HasForeignKey(ab => ab.BookmarkId);
            */
        }
    }
}