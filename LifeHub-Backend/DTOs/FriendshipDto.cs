namespace LifeHub.DTOs
{
    public class FriendshipDto
    {
        public int Id { get; set; }
        public string RequesterId { get; set; } = null!;
        public string ReceiverId { get; set; } = null!;
        public int Status { get; set; } // FriendshipStatus as int
        public DateTime CreatedAt { get; set; }
        public UserDto? Requester { get; set; }
        public UserDto? Receiver { get; set; }
    }

    public class CreateFriendshipDto
    {
        public string ReceiverId { get; set; } = null!;
    }

    public class UpdateFriendshipDto
    {
        public int Status { get; set; } // 0=Pending, 1=Accepted, 2=Rejected, 3=Blocked
    }
}
