namespace Blog.Application.Core.Categories.Commands
{
    public class CategoryUpdateCommand : CategoryCommand
    {
        public int Id { get; set; }

        public CategoryUpdateCommand(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
