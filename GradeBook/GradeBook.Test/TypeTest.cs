using System;
using Xunit;

namespace GradeBook.Test
{
    public class TypeTest
    {
        [Fact]
        public void ValueTypeAlsoPassByValue()
        {
            //Arrange & Act
            var x = GetInt();
            SetInt(ref x);
            //Assert
            Assert.NotEqual(3, x);
            Assert.Equal(42, x);
        }

        private void SetInt(ref int x)
        {
            x=42;
        }

        [Fact]
        public void StringsBehavesLikeValueTypes()
        {
            string name = "Prashant";
            var upper=MakeUpper(name);

            //Assert
            Assert.Equal("Prashant", name);
        }

        private string MakeUpper(string name)
        {
            return name.ToUpper();
        }

        private int GetInt()
        {
            return 3;
        }

        [Fact]
        public void CSharpCanPassByReference()
        {
            //Arrange
            var book1 = GetBook("Book 1");
            GetBookSetName(out book1, "New Book Name");

            //Act

            //Assert
            Assert.Equal("New Book Name", book1.Name);
        }
        private void GetBookSetName(out InMemoryBook  book, string name)
        {
            book = new InMemoryBook (name);
        }
        [Fact]
        public void CSharpIsPassByValue()
        {
            //Arrange
            var book1 = GetBook("Book 1");
            GetBookSetName(book1, "New Book Name");

            //Assert
            Assert.Equal("Book 1", book1.Name);
            Assert.NotEqual("New Book Name", book1.Name);
        }

        private void GetBookSetName(InMemoryBook  book, string name)
        {
            book=new InMemoryBook (name);
        }
        [Fact]
        public void CanSetNameFromReference()
        {
            //Arrange
            var book1 = GetBook("Book 1");
            SetName(book1 ,"New Book Name");

            //Assert
            Assert.NotEqual("Book 1", book1.Name);
            Assert.Equal("New Book Name", book1.Name);
        }

        private void SetName(InMemoryBook  book, string name)
        {
            book.Name=name;
        }

        [Fact]
        public void GetBookReturnsDifferentObject()
        {
            //Arrange
            var book1 = GetBook("Book 1");
            var book2 = GetBook("Book 2");

            //Act

            //Assert
            Assert.Equal("Book 1", book1.Name);
            Assert.Equal("Book 2", book2.Name);
            Assert.NotSame(book1, book2);
        }
        [Fact]
        public void TwoVarsReferenceSameObject()
        {
            //Arrange
            var book1 = GetBook("Book 1");
            var book2 = book1;

            //Act

            //Assert
            Assert.Same(book1, book2);
            Assert.True(object.ReferenceEquals(book1, book2));
        }

        InMemoryBook  GetBook(string name)
        {
            return new InMemoryBook (name);
        }
        [Fact]
        public void WriteLogDelegateCanPointToMethod()
        {
            //Arrange
            WriteLogDelegate Log=ReturnMessage;
            //
            Log += ReturnMessage;
            Log += HelperMessage;
            var result = Log("Hello");

            //Assert
            Assert.Equal("HELLO", result);
            Assert.Equal(3, count);

        }

        string HelperMessage(string message)
        {
            count++;
            return message.ToUpper();
        }
        string ReturnMessage(string message)
        {
            count++;
            return message;
        }
        private int count = 0;
    }

    public delegate string WriteLogDelegate(string logMessage);
}
