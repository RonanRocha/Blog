namespace Blog.Application.Core.ViewModels
{
    public record PostViewModel 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; } 
        public string Content { get; set; }
        public UserViewModel User { get; set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }
    }
}
