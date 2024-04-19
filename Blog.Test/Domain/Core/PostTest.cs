using Blog.Domain.Core.Entities;
using Blog.Domain.Validation;
using FluentAssertions;

namespace Blog.Test.Domain.Core
{
    public class PostTest
    {

        public static IEnumerable<object[]> BadPosts =>
         new List<object[]>()
         {
              new object[] { "",1, "image/path", "Titulo", "Conteudo do Post" },
              new object[] { null, 1, "image/path", "Titulo", "Conteudo do post" },
              new object[] { "15a1sda1sd51asdasd51532", -1, "image/path", "Titulo", "Conteudo do post" },
              new object[] { "15a1sda1sd51asdasd51532", 0, "image/path", "Titulo", "Conteudo do post" },
              new object[] { "15a1sda1sd51asdasd51532", 1, "image/path", "", "Conteudo do post" },
              new object[] { "15a1sda1sd51asdasd51532", 1, "image/path", null, "Conteudo do post" },
              new object[] { "15a1sda1sd51asdasd51532", 1, "image/path", "a", "Conteudo do post" },
              new object[] { "15a1sda1sd51asdasd51532", 1, "image/path", "ab", "Conteudo do post" },
              new object[] { "15a1sda1sd51asdasd51532", 1, "image/path", "Titulo", "" },
              new object[] { "15a1sda1sd51asdasd51532", 1, "image/path", "Titulo", null },
              new object[] { "15a1sda1sd51asdasd51532", 1, "image/path", "Titulo", "a" },
              new object[] { "15a1sda1sd51asdasd51532", 1, "image/path", "Titulo", "ab" },
              new object[] { "15a1sda1sd51asdasd51532", 1, "", "Titulo", "abasdasdasd ausdhaus" },
              new object[] { "15a1sda1sd51asdasd51532", 1, null, "Titulo", "abasdasdasd ausdhaus" },
              new object[] { "15a1sda1sd51asdasd51532", 1, null, @"Lorem ipsum dolor sit amet,
                     consectetur adipiscing elit.
                     Suspendisse quis tortor in mauris aliquet hendrerit quis sit amet risus.
                     Vivamus molestie diam sed rhoncus tincidunt.
                     Duis ut dolor sit amet arcu accumsan tristique.
                     Curabitur sem sem, cursus vel in. ", "abasdasdasd ausdhaus"},
              new object[] { "15a1sda1sd51asdasd51532", 1, null, @"Lorem ipsum dolor sit amet,
                     consectetur adipiscing elit.
                     Suspendisse quis tortor in mauris aliquet hendrerit quis sit amet risus.
                     Vivamus molestie diam sed rhoncus tincidunt.
                     Duis ut dolor sit amet arcu accumsan tristique.
                     Curabitur sem sem, cursus vel in. ", "abasdasdasd ausdhaus" },
         };


        [Theory]
        [InlineData("15a1sda1sd51asdasd51532", 1, "image/path", "Titulo", "Conteudo do post")]
        public void Post_Should_Be_Construct(string userId, int? categoryId, string image, string title, string content)
        {
            Post post = new Post(userId, categoryId, image, title, content);
            post.Title.Should().Be(title);
            post.Image.Should().Be(image);
            post.CategoryId.Should().Be(categoryId);
            post.UserId.Should().Be(userId);
            post.Content.Should().Be(content);
        }

        [Theory]
        [MemberData(nameof(BadPosts))]
        public void Post_Construct_Should_Throw_Exception(string userId, int? categoryId, string image, string title, string content)
        {
            Action action = () => new Post(userId, categoryId, image, title, content);
            action.Should().Throw<DomainValidationException>();
        }

        [Theory]
        [MemberData(nameof(BadPosts))]
        public void Post_Update_Should_Throw_Exception(string userId, int? categoryId, string image, string title, string content)
        {
            Post post = new Post("1a86s4das1da51", 1, "image.png", "titulo", "conteudo");
            Action action = () => post.UpdatePost(userId, categoryId, image, title, content);
            action.Should().Throw<DomainValidationException>(); 
        }

        [Theory, MemberData(nameof(BadPosts))]
        public void Post_Validate_Should_Throw_Exception(string userId, int? categoryId, string image, string title, string content)
        {
            Post post = new Post("1a86s4das1da51", 1, "image.png", "titulo", "conteudo");
            Action action = ()  => post.ValidatePost(userId,categoryId, image, title, content);
            action.Should().Throw<DomainValidationException>();
        }

        [Theory]
        [InlineData("51as1d5as1dasd51", 1,"image.png", "title", "content")]
        public void Post_Should_Updated(string userId, int? categoryId, string image, string title, string content)
        {
            Post post = new Post("1a86s4das1da51", 1, "image.png", "titulo", "conteudo");

            post.UpdatePost(userId, categoryId, image,title, content);


            post.UserId.Should().Be(userId);
            post.CategoryId.Should().Be(categoryId);
            post.Image.Should().Be(image);
            post.Title.Should().Be(title);
            post.Content.Should().Be(content);
            post.UpdatedAt.Should().NotBeNull();

        }


    }
}

