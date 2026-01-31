namespace LifeHub.Models
{
    /// <summary>
    /// Modelo para metadatos de archivos de música
    /// Los archivos reales se almacenan localmente en el dispositivo del usuario
    /// </summary>
    public class MusicFile
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? Artist { get; set; }
        public string? Album { get; set; }
        public int? DurationSeconds { get; set; }
        public string? Genre { get; set; }
        public string? LocalPath { get; set; } // Path relativo en el cliente
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navegación
        public ApplicationUser User { get; set; } = null!;
    }
}
