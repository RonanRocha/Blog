using Blog.Domain.Core.Entities;
using Blog.Domain.Validation;
using FluentAssertions;

namespace Blog.Test.Domain.Core
{
    public  class CommentTest
    {
        [Theory]
        [InlineData("16151asdasd9456a1sd91asd45123sd1a", 1, "Novo comentário")]
        [InlineData("549884asd8454asd894asd165a4sd546a", 100, "Novo asdasd")]
        public void Comment_Sould_Be_Construct(string userId, int postId, string message)
        {
            Comment comment = new Comment(userId, postId, message);
            comment.UserId.Should().Be(userId);
            comment.PostId.Should().Be(postId);
            comment.Message.Should().Be(message);

        }


        [Theory]
        [InlineData("", 1, "Novo comentário")]
        [InlineData("549884asd8454asd894asd165a4sd546a", 0, "Novo asdasd")]
        [InlineData("16151asdasd9456a1sd91asd45123sd1a", 1, null)]
        [InlineData("549884asd8454asd894asd165a4sd546a", -1, "Novo asdasd")]
        [InlineData("16151asdasd9456a1sd91asd45123sd1a", 1, "N")]
        [InlineData("549884asd8454asd894asd165a4sd546a", 100, "No")]
        [InlineData(null, 1, "Novo comentário")]
        [InlineData("549884asd8454asd894asd165a4sd546a", 100, @"Lorem ipsum dolor sit amet,
                     consectetur adipiscing elit.
                     Suspendisse quis tortor in mauris aliquet hendrerit quis sit amet risus.
                     Vivamus molestie diam sed rhoncus tincidunt.
                     Duis ut dolor sit amet arcu accumsan tristique.
                     Curabitur sem sem, cursus vel in. ")]
        public void Comment_Construct_Should_Throw_Exception(string userId, int postId, string message)
        {
            Action action = () => new Comment(userId, postId, message);
            action.Should().Throw<DomainValidationException>();
        }

        [Theory]
        [InlineData("", 1, "Novo comentário")]
        [InlineData("549884asd8454asd894asd165a4sd546a", 0, "Novo asdasd")]
        [InlineData("16151asdasd9456a1sd91asd45123sd1a", 1, null)]
        [InlineData("549884asd8454asd894asd165a4sd546a", -1, "Novo asdasd")]
        [InlineData("16151asdasd9456a1sd91asd45123sd1a", 1, "N")]
        [InlineData("549884asd8454asd894asd165a4sd546a", 100, "No")]
        [InlineData(null, 1, "Novo comentário")]
        [InlineData("549884asd8454asd894asd165a4sd546a", 100, @"Lorem ipsum dolor sit amet,
                     consectetur adipiscing elit.
                     Suspendisse quis tortor in mauris aliquet hendrerit quis sit amet risus.
                     Vivamus molestie diam sed rhoncus tincidunt.
                     Duis ut dolor sit amet arcu accumsan tristique.
                     Curabitur sem sem, cursus vel in. ")]
        public void Comment_Validate_Shoud_Throw_Exception(string userId, int postId, string message)
        {
            Comment comment = new Comment("5a1sd9as1das51", 1, "asdasdasdas");
            Action action = () => comment.ValidateComment(userId, postId, message);
            action.Should().Throw<DomainValidationException>();
        }

        [Theory]
        [InlineData("")]
        [InlineData("N")]
        [InlineData("Na")]
        [InlineData(null)]
        [InlineData(@"Lorem ipsum dolor sit amet,
                     consectetur adipiscing elit.
                     Suspendisse quis tortor in mauris aliquet hendrerit quis sit amet risus.
                     Vivamus molestie diam sed rhoncus tincidunt.
                     Duis ut dolor sit amet arcu accumsan tristique.
                     Curabitur sem sem, cursus vel in. ")]
   
        public void Comment_Update_Should_Throw_Exception(string message)
        {
            Comment comment = new Comment("5a1sd9as1das51", 1, "asdasdasdas");
            Action action = () => comment.Update(message);
            action.Should().Throw<DomainValidationException>();
        }

        [Theory]
        [InlineData("Novo comentario no post")]
        public void Comment_Update_Should_Updated_Message(string message)
        {
            Comment comment = new Comment("5a1sd9as1das51", 1, "asdasdasdas");
            comment.Update(message);

            comment.Message.Should().Be(message);
            comment.UpdatedAt.Should().NotBeNull();
        }


    }
}
