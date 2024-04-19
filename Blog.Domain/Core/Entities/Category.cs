using Blog.Domain.Validation;

namespace Blog.Domain.Core.Entities
{
    public class Category : Entity
    {
        public Category(string name)
        {
          
            ValidateCategory(name);
            Name = name;
            CreatedAt = DateTime.UtcNow;
        }

        public string Name { get; private set; }
        public List<Post> Posts { get; set; }

        public void UpdateCategory(string name)
        {
            ValidateCategory(name);
            Name = name;
            UpdatedAt = DateTime.UtcNow;    
        }

        public  void ValidateCategory (string name)
        {
            DomainValidationException.When(String.IsNullOrEmpty(name), $"Name is required");
            DomainValidationException.When(name.Length < 3, $"Minimum 3 characters for name");
            DomainValidationException.When(name.Length > 255, $"Maximum 255 characters for name");
        }


    }
}
