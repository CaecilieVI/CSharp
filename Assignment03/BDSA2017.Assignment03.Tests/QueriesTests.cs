using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BDSA2017.Assignment03.Tests
{
    public class QueriesTests
    {
        private readonly IEnumerable<Wizard> _wizards;

        public QueriesTests()
        {
            _wizards = new List<Wizard>
            {
                new Wizard(){ Name = "Harry Potter", Medium = "Harry Potter and the Philosopher's Stone", Year = 1997, Author = "J. K. Rowling"},
                new Wizard(){ Name = "Hermione", Medium = "Harry Potter and the Philosopher's Stone", Year = 1997, Author = "J. K. Rowling"},
                new Wizard(){ Name = "Gandalf", Medium = "The Hobbit", Year = 1937, Author = "Tolkien"},
                new Wizard(){ Name = "Merlin", Medium = "Folklore", Year = null, Author = "Unknown"},
                new Wizard(){ Name = "Darth Vader", Medium = "Star Wars", Year = 1977, Author = "George Lucas"},
                new Wizard(){ Name = "Darth Sidious", Medium = "Star Wars", Year = 1980, Author = "George Lucas"},
                new Wizard(){ Name = "Luna Lovegood", Medium = "Harry Potter and the Order of the Phoenix", Year = 2003, Author = "J. K. Rowling"},
                new Wizard(){ Name = "Dr. Strange", Medium = "Strange Tales #110", Year = 1963, Author = "Stan Lee"},
                new Wizard(){ Name = "Harry Dresden", Medium = "Dresden Files", Year = 2000, Author = "Jim Butcher"},
                new Wizard(){ Name = "Ricewind", Medium = "Discworld", Year = 1983, Author = "Terry Pratchett"}
            };
        }

        [Fact]
        public void RowlingWizardsAfter2002Extensions_given_wizards_returns_luna_lovegood()
        {
            //Arrange
            var expected = "Luna Lovegood";

            //Act
            var result = Queries.RowlingWizardsAfter2002Extension(_wizards);

            //Assert
            Assert.Equal(expected,result.Single());
            
        }

        [Fact]
        public void RowlingWizardsAfter2002LINQ_given_wizards_returns_luna_lovegood()
        {
            //Arrange
            var expected = "Luna Lovegood";

            //Act
            var result = Queries.RowlingWizardsAfter2002Linq(_wizards);

            //Assert
            Assert.Equal(expected, result.Single());

        }

        [Fact]
        public void YearOfFirstSithExtension_given_list_returns_1977()
        {
            //Arrange
            var expected = 1977;

            //Act
            var result = Queries.YearOfFirthSithExtension(_wizards);

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void YearOfFirstSithLINQ_given_list_returns_1977()
        {
            //Arrange
            var expected = 1977;

            //Act
            var result = Queries.YearOfFirthSithLinq(_wizards);

            //Assert
            Assert.Equal(expected, result);
        }

        

        [Fact]
        public void ListOfHarryPotterBooksExtension_given_list_returns_only_harry_potter_books()
        {
            //Arrange
            var expected = new (string Medium, int? Year)[]
            {
                ("Harry Potter and the Philosopher's Stone", 1997),
                ("Harry Potter and the Order of the Phoenix", 2003)
            };

            //Act
            var result = Queries.ListOfHarryPotterBooksExtension(_wizards);

            //Assert
            Assert.Equal(expected, result);
        }

         

        [Fact]
        public void ListOfHarryPotterBooksLinq_given_list_returns_only_harry_potter_books()
        {
            //Arrange
            var expected = new(string Medium, int? Year)[]
            {
                ("Harry Potter and the Philosopher's Stone", 1997),
                ("Harry Potter and the Order of the Phoenix", 2003)
            };

            //Act
            var result = Queries.ListOfHarryPotterBooksLinq(_wizards);

            //Assert
            Assert.Equal(expected, result);
        }
       
        [Fact]
        public void ListOfWizardNamesReverseOrderByAuthorExtension_returns_correct_order()
        {
            //Arrange
            var expected = new[]
            {
                "Merlin",
                "Gandalf",
                "Ricewind",
                "Dr. Strange",
                "Harry Dresden",
                "Harry Potter","Hermione", "Luna Lovegood",
                "Darth Vader", "Darth Sidious"
            };

            //Act
            var result = Queries.ListOfWizardNamesReverseOrderByAuthorExtension(_wizards);

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ListOfWizardNamesReverseOrderByAuthorLinq_returns_correct_order()
        {
            //Arrange
            var expected = new[]
            {
                "Merlin",
                "Gandalf",
                "Ricewind",
                "Dr. Strange",
                "Harry Dresden",
                "Harry Potter","Hermione", "Luna Lovegood",
                "Darth Vader", "Darth Sidious"
            };

            //Act
            var result = Queries.ListOfWizardNamesReverseOrderByAuthorLinq(_wizards);

            //Assert
            Assert.Equal(expected, result);
        }
    }
}
