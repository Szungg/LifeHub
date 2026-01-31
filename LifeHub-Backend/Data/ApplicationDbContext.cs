using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LifeHub.Models;

namespace LifeHub.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }
        public DbSet<RecommendationRating> RecommendationRatings { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<MusicFile> MusicFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ============ FRIENDSHIPS ============
            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.Requester)
                .WithMany(u => u.FriendshipsInitiated)
                .HasForeignKey(f => f.RequesterId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.Receiver)
                .WithMany(u => u.FriendshipsReceived)
                .HasForeignKey(f => f.ReceiverId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Friendship>()
                .HasIndex(f => new { f.RequesterId, f.ReceiverId })
                .IsUnique();

            // ============ MESSAGES ============
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Message>()
                .HasIndex(m => new { m.SenderId, m.ReceiverId });

            // ============ RECOMMENDATIONS ============
            modelBuilder.Entity<Recommendation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Recommendations)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // ============ RECOMMENDATION RATINGS ============
            modelBuilder.Entity<RecommendationRating>()
                .HasOne(rr => rr.Recommendation)
                .WithMany(r => r.Ratings)
                .HasForeignKey(rr => rr.RecommendationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RecommendationRating>()
                .HasOne(rr => rr.User)
                .WithMany(u => u.RecommendationRatings)
                .HasForeignKey(rr => rr.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RecommendationRating>()
                .HasIndex(rr => new { rr.RecommendationId, rr.UserId })
                .IsUnique();

            // ============ DOCUMENTS ============
            modelBuilder.Entity<Document>()
                .HasOne(d => d.User)
                .WithMany(u => u.Documents)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // ============ MUSIC FILES ============
            modelBuilder.Entity<MusicFile>()
                .HasOne(m => m.User)
                .WithMany(u => u.MusicFiles)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
