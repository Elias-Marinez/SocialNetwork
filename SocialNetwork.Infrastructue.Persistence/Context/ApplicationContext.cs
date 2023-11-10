
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Infrastructure.Persistence.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Tables & Primary Keys
            modelBuilder.Entity<User>()
                .ToTable("Users")
                .HasKey(u => u.UserId);

            modelBuilder.Entity<Post>()
                .ToTable("Posts")
                .HasKey(p => p.PostId);

            modelBuilder.Entity<Comment>()
                .ToTable("Comments")
                .HasKey(c => c.CommentId);
            #endregion

            #region Relationships
             modelBuilder.Entity<User>()
                .HasMany<Post>(u => u.Posts)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany<Comment>(p => p.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Friends)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "Friends",
                    a => a.HasOne<User>().WithMany().HasForeignKey("UserId"),
                    b => b.HasOne<User>().WithMany().HasForeignKey("FriendId"),
                    x =>
                    {
                        x.HasKey("UserId", "FriendId");
                    });

            modelBuilder.Entity<Post>()
                .HasMany<Comment>(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Users
            modelBuilder.Entity<User>()
                .Property(user => user.Name)
                .HasMaxLength(30);

            modelBuilder.Entity<User>()
                .Property(user => user.LastName)
                .HasMaxLength(30);

            modelBuilder.Entity<User>()
                .Property(user => user.Phone)
                .HasMaxLength(12);

            modelBuilder.Entity<User>()
                .Property(user => user.Email)
                .HasMaxLength(60);

            modelBuilder.Entity<User>()
                .Property(user => user.Username)
                .HasMaxLength(40);
            #endregion

        }
    }
}
