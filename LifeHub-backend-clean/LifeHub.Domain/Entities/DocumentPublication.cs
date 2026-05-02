using System;
namespace LifeHub.Domain.Entities
{
	public class DocumentPublication
	{
		public int Id { get; set; }
		public int DocumentId { get; set; }
		public DateTime PublishedAt { get; set; } = DateTime.UtcNow;

		public Document Document { get; set; } = null!;
	}
}