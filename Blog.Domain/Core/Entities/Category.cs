namespace Blog.Domain.Core.Entities
{
    public class Category : Entity
    {
        public Category(string name)
        {
            Name = name;
            CreatedAt = DateTime.UtcNow;
        }

        public string Name { get; private set; }
        public List<Post> Posts { get; set; }

        public void UpdateCategory(string name)
        {
            Name = name;
            UpdatedAt = DateTime.UtcNow;    
        }

    }
}
