namespace LifeHub.Domain.Entities.Documents
{
    public class Document
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public int? CreativeSpaceId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DocumentType Type { get; set; }
        public bool IsPublic { get; set; } = false;
        public DateTime? PublishedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ApplicationUser User { get; set; } = null!;
        public CreativeSpace? CreativeSpace { get; set; }
        public ICollection<DocumentVersion> Versions { get; set; } = new List<DocumentVersion>();
        public DocumentPublication? Publication { get; set; }
    }

    public enum DocumentType
    {
        Note = 0,
        TextFile = 1,
        List = 2
    }
}
