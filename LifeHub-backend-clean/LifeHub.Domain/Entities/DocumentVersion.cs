using System;
namespace LifeHub.Domain.Entities
{
	public class DocumentVersion
	{
		public int Id { get; set; }
		public int DocumentId { get; set; }
		public string UserId { get; set; } = null!;
		public string Content { get; set; } = string.Empty;
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		public Document Document { get; set; } = null!;
		public ApplicationUser User { get; set; } = null!;
	}
}