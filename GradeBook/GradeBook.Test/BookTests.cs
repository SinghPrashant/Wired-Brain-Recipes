using System;
using Xunit;

namespace GradeBook.Test
{
    public class BookTests
    {
        [Fact]
        public void Test1()
        {
            //arrange
            var book = new InMemoryBook ("Prashant's Grade Book");
            book.AddGrade(89.1);
            book.AddGrade(90.5);
            book.AddGrade(77.3);

            //act
            var result = book.GetStats();

            //assert
            Assert.Equal(85.6, result.Average,1);
            Assert.Equal(90.5, result.HighestGrade,1);
            Assert.Equal(77.3, result.LowestGrade,1);
            Assert.Equal('B', result.LetterGrade);
        }
        [Fact]
        public void GradeFrom0To100Allowed()
        {
            //Arrange
            var book = new InMemoryBook ("Prashant's GradeBook");

            //Act
            //book.AddGrade(105.5);
            book.AddGrade(95.5);
            book.AddGrade(9);
            var stats=book.GetStats();
            //Assert
            Assert.Equal(95.5, stats.HighestGrade);
            Assert.NotEqual(105.5, stats.HighestGrade);
            Assert.Equal(9, stats.LowestGrade);
        }
    }
}
