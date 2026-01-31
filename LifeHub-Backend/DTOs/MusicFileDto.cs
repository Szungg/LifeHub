namespace LifeHub.DTOs
{
    public class MusicFileDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? Artist { get; set; }
        public string? Album { get; set; }
        public int? DurationSeconds { get; set; }
        public string? Genre { get; set; }
        public string? LocalPath { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateMusicFileDto
    {
        public string FileName { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? Artist { get; set; }
        public string? Album { get; set; }
        public int? DurationSeconds { get; set; }
        public string? Genre { get; set; }
        public string? LocalPath { get; set; }
    }

    public class UpdateMusicFileDto
    {
        public string Title { get; set; } = null!;
        public string? Artist { get; set; }
        public string? Album { get; set; }
        public int? DurationSeconds { get; set; }
        public string? Genre { get; set; }
    }
}
