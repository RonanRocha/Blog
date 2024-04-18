using Blog.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Data.Mappings
{
    public class PostMapConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId).IsRequired();

            builder.Property(x => x.CategoryId);

            builder.Property(x => x.Image)
                   .HasMaxLength(2083)
                   .IsRequired();

            builder.Property(x => x.Title)
                   .HasMaxLength(255)
                   .IsRequired();
                    
            builder.Property(x => x.Content)
                   .HasMaxLength(500)
                   .IsRequired();

            builder.Property(x => x.CreatedAt)
                   .IsRequired();

            builder.HasMany(x => x.Comments)
                   .WithOne(x => x.Post)
                   .HasForeignKey(x => x.PostId)
                   .OnDelete(DeleteBehavior.ClientCascade);

        }
    }
}
