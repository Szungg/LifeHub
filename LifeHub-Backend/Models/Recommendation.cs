namespace LifeHub.Models
{
    /// <summary>
    /// Modelo para recomendaciones de películas y libros
    /// </summary>
    public class Recommendation
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public RecommendationType Type { get; set; } // Movie, Book
        public string? Genre { get; set; }
        public string? Author { get; set; }
        public int? Year { get; set; }
        public string? ExternalLink { get; set; } // IMDb, Goodreads, etc.
        public string? CoverImageUrl { get; set; }
        public double AverageRating { get; set; } = 0;
        public int TotalRatings { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navegación
        public ApplicationUser User { get; set; } = null!;
        public ICollection<RecommendationRating> Ratings { get; set; } = new List<RecommendationRating>();
    }

    public enum RecommendationType
    {
        Movie = 0,
        Book = 1,
        Series = 2
    }
}
