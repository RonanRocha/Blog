namespace Blog.Domain.Core.Entities
{
    public class Post : Entity
    {
        public Post(string userId, int categoryId, string image, string title, string content)
        {
            UserId = userId;
            CategoryId = categoryId;
            Image = image;
            Title = title;
            Content = content;
            CreatedAt = DateTime.UtcNow;
        }

        public string UserId { get; private set; }
        public int CategoryId { get; private set; }
        public string Image { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public User User { get; set; }
        public List<Comment> Comments { get; set; }
        public Category Category { get; set; }

        public void UpdatePost(string userId, int categoryId, string image, string title, string content)
        {
            UserId = userId;
            CategoryId = categoryId;
            Image = image;
            Title = title;
            Content = content;
            UpdatedAt = DateTime.UtcNow;

        }

    }
}
