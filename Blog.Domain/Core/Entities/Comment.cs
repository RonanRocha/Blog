namespace Blog.Domain.Core.Entities
{
    public class Comment : Entity
    {
        public Comment(string userId, int postId, string message)
        {
            UserId = userId;
            PostId = postId;
            Message = message;
            CreatedAt = DateTime.UtcNow;
        }

        public string UserId { get; private set; }
        public int PostId { get; private set; }
        public string Message { get; private set; }
        public User User { get; private set; }
        public Post Post { get; private set; }

        public void Update(string message)
        {

            Message = message;
            UpdatedAt = DateTime.UtcNow;
        }

    }
}
