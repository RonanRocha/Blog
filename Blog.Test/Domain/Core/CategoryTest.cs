using Blog.Domain.Core.Entities;
using Blog.Domain.Validation;
using FluentAssertions;

namespace Blog.Test.Domain.Core
{
    public class CategoryTest
    {
        [Theory]
        [InlineData("Usuário")]
        [InlineData("Fulano de tal")]
        public void Category_Should_Be_Construct(string name)
        {
            Category category = new Category(name);
            category.Name.Should().Be(name);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("ab")]
        [InlineData("ac")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(@"Lorem ipsum dolor sit amet,
                     consectetur adipiscing elit.
                     Suspendisse quis tortor in mauris aliquet hendrerit quis sit amet risus.
                     Vivamus molestie diam sed rhoncus tincidunt.
                     Duis ut dolor sit amet arcu accumsan tristique.
                     Curabitur sem sem, cursus vel in. ")]
        public void Category_Constructor_Should_Throw_Exception(string name)
        {
            Action a = () => new Category(name);
            a.Should().Throw<DomainValidationException>();

        }

        [Theory]
        [InlineData("Categoria atualizada")]
        public void Category_Should_Updated(string name)
        {
            Category category = new Category("Nova categoria");
            category.UpdateCategory(name);
            category.UpdatedAt.Should().NotBeNull();
            category.Name.Should().Be(name);

        }

        [Theory]
        [InlineData("a")]
        [InlineData("ab")]
        [InlineData("ac")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(@"Lorem ipsum dolor sit amet,
                     consectetur adipiscing elit.
                     Suspendisse quis tortor in mauris aliquet hendrerit quis sit amet risus.
                     Vivamus molestie diam sed rhoncus tincidunt.
                     Duis ut dolor sit amet arcu accumsan tristique.
                     Curabitur sem sem, cursus vel in. ")]
        public void Category_Update_Should_Throw_Exception(string name)
        {
            Category category = new Category("Nova categoria");
            Action a = () => category.UpdateCategory(name);

            a.Should().Throw<DomainValidationException>();

        }

        [Theory]
        [InlineData("a")]
        [InlineData("ab")]
        [InlineData("ac")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(@"Lorem ipsum dolor sit amet,
                     consectetur adipiscing elit.
                     Suspendisse quis tortor in mauris aliquet hendrerit quis sit amet risus.
                     Vivamus molestie diam sed rhoncus tincidunt.
                     Duis ut dolor sit amet arcu accumsan tristique.
                     Curabitur sem sem, cursus vel in. ")]
        public void Category_Validate_Should_Throw_Exception(string name)
        {
            Category category = new Category("Nova categoria");
            Action a = () => category.ValidateCategory(name);
            a.Should().Throw<DomainValidationException>();

        }


    }
}
