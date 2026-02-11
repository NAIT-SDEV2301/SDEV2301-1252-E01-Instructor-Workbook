using Xunit;
using Lesson09_UnitTesting.Models;

namespace Lesson09_UnitTesting.Tests;

public class PartA_BookTests
{
    [Fact]
    public void Ctor_WithValidValues_SetsProperties()
    {
        // Arrange
        var title = "Clean Code";
        var pages = 464;

        // Act
        var book = new Book(title, pages);

        // Assert
        Assert.Equal(title, book.Title);
        Assert.Equal(pages, book.Pages);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Ctor_WhenTitleIsBlank_ThrowsArgumentException(string? title)
    {
        var ex = Assert.Throws<ArgumentException>(() => new Book(title!, 10));
        Assert.Contains("Title", ex.Message);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Ctor_WhenPagesNotPositive_ThrowsArgumentOutOfRangeException(int pages)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Book("Any", pages));
    }
}
