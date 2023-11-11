
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Infrastructure.Persistence.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options) { }

        public DbSet<Friends> Friends { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Tables & Primary Keys

            modelBuilder.Entity<Friends>()
                .ToTable("Friends")
                .HasKey(p => p.FriendsId);

            modelBuilder.Entity<Post>()
                .ToTable("Posts")
                .HasKey(p => p.PostId);

            modelBuilder.Entity<Comment>()
                .ToTable("Comments")
                .HasKey(c => c.CommentId);
            #endregion

            #region Relationships

            modelBuilder.Entity<Post>()
                .HasMany<Comment>(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
