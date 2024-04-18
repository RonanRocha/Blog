using Blog.Domain.Validation;

namespace Blog.Domain.Core.Entities
{
    public class Post : Entity
    {
        public Post(string userId, int? categoryId, string image, string title, string content)
        {
            ValidatePost(userId, categoryId, image, title, content);

            UserId = userId;
            CategoryId = categoryId;
            Image = image;
            Title = title;
            Content = content;
            CreatedAt = DateTime.UtcNow;
        }

        public string UserId { get; private set; }
        public int? CategoryId { get; private set; }
        public string Image { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public User User { get; set; }
        public List<Comment> Comments { get; set; }
        public Category Category { get; set; }

        public void UpdatePost(string userId, int? categoryId, string image, string title, string content)
        {

            ValidatePost(userId, categoryId, image, title, content);

            UserId = userId;
            CategoryId = categoryId;
            Title = title;
            Content = content;
            UpdatedAt = DateTime.UtcNow;
            
            if(!String.IsNullOrEmpty(image))
                Image = image;

        }

        private void ValidatePost(string userId, int? categoryId, string image, string title, string content)
        {
            DomainValidationException.When(string.IsNullOrEmpty(userId), "UserId is required");
            DomainValidationException.When(categoryId <= 0, "CategoryId must be a positive number");
            DomainValidationException.When(string.IsNullOrEmpty(title), "Title is required");
            DomainValidationException.When(title.Length < 3, "Title required min 3 characters");
            DomainValidationException.When(title.Length >  255, "Title required max 255 characters");
            DomainValidationException.When(content.Length < 3, "Content required min 3 characters");
            DomainValidationException.When(content.Length > 500, "Content required max 255 characters");
            DomainValidationException.When(string.IsNullOrEmpty(image), "Image Path is required");
            DomainValidationException.When(image.Length > 2083, "Image required max 2083 characters");
        }

    }
}
