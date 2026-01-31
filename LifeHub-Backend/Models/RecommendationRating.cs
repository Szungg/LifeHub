namespace LifeHub.Models
{
    /// <summary>
    /// Modelo para calificaciones de recomendaciones
    /// </summary>
    public class RecommendationRating
    {
        public int Id { get; set; }
        public int RecommendationId { get; set; }
        public string UserId { get; set; } = null!;
        public int Rating { get; set; } // 1-5
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navegación
        public Recommendation Recommendation { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
    }
}
