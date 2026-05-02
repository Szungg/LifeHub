namespace LifeHub.Domain.Entities.Friendships
{
    public class Friendship
    {
        public int Id { get; set; }
        public string RequesterId { get; set; } = null!;
        public string ReceiverId { get; set; } = null!;
        public FriendshipStatus Status { get; set; } = FriendshipStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ApplicationUser Requester { get; set; } = null!;
        public ApplicationUser Receiver { get; set; } = null!;
    }

    public enum FriendshipStatus
    {
        Pending = 0,
        Accepted = 1,
        Rejected = 2,
        Blocked = 3
    }
}
