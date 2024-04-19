using Blog.Domain.Validation;

namespace Blog.Domain.Core.Entities
{
    public class Comment : Entity
    {
        public Comment(string userId, int postId, string message)
        {
            ValidateComment(userId, postId, message);

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
            ValidateComment(this.UserId, this.PostId, message);
            Message = message;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ValidateComment(string userId, int postId, string message)
        {
            DomainValidationException.When(string.IsNullOrEmpty(userId), "UserId is required");
            DomainValidationException.When(postId <= 0 , "PostId must be positive");
            DomainValidationException.When(string.IsNullOrEmpty(message), "Message is required");
            DomainValidationException.When(message.Length < 3, "Min characters for message is 3");
            DomainValidationException.When(message.Length > 255, "Max characters for message is 255");
        }

    }
}
