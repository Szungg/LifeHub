using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LifeHub.Domain.Entities;

namespace LifeHub.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<AllowedWebsite> AllowedWebsites { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<CreativeSpace> CreativeSpaces { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentPublication> DocumentPublications { get; set; }
        public DbSet<DocumentVersion> DocumentVersions { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MusicFile> MusicFiles { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }
        public DbSet<RecommendationRating> RecommendationRatings { get; set; }
        public DbSet<SpacePermission> SpacePermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // ...existing code...
        }
    }
}
