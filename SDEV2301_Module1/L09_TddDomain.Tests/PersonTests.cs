namespace L09_TddDomain.Tests
{
    public class PersonTests
    {
        [Fact]
        public void DefaultConstructor_ShouldSetUnknownNames_AndNullEmail()
        {
            // Arrange + Act
            var person = new Person();

            // Assert
            Assert.Equal("Unknown", person.FirstName);
            Assert.Equal("Unknown", person.LastName);
            Assert.Null(person.Email);

        }

        [Fact]
        public void GreedyConstructor_ShouldTrimNames()
        {
            // Arrange + Act
            var person = new Person("  Don  ", "  Welch  ", null);

            // Assert
            Assert.Equal("Don", person.FirstName);
            Assert.Equal("Welch", person.LastName);

        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void GreedyConstructor_ShouldThrow_WhenFirstNameInvalid(string? firstName)
        {
            // Arrange + Act + Assert
            var ex = Assert.Throws<ArgumentNullException>(() =>
                new Person(firstName!, "Welch", null));
            Assert.Contains("is required", ex.Message);

        }

        [Fact]
        public void FullName_ShouldReturn_LastCommaFirst()
        {
            // Arrange + Act
            var person = new Person("Don", "Welch", null);
            // Assert
            Assert.Equal("Welch, Don", person.FullName);
        }
    }
}
