using BookStoreAPI.Model;
using BookStoreAPI.Services.Dapper;
using BookStoreAPI.Services.Repositories;
using Dapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreAPI.Test.Services.Test
{
    public class BookRepositoryTest
    {
        private readonly Mock<IDapper> _dapperMock;
        private readonly BookRepository _bookRepository;

        public BookRepositoryTest()
        {
            _dapperMock = new Mock<IDapper>();
            _bookRepository = new BookRepository(_dapperMock.Object, new List<BookModel>());
        }
        [Fact]
        public async Task AddBook_ShouldReturnTrue()
        {
            // Arrange
            var book = new BookModel { Title = "New Book", Author = "Author", PublishedYear = 2024 };
            _dapperMock.Setup(d => d.ExecuteAsync(It.IsAny<string>(), It.IsAny<DynamicParameters>(), null,CommandType.Text))
           .ReturnsAsync(1);
            // Act
            var result = await _bookRepository.AddBook(book);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteBook_ShouldReturnTrue()
        {
            // Arrange
            int bookId = 1;
            _dapperMock.Setup(d => d.ExecuteAsync(It.IsAny<string>(), It.IsAny<DynamicParameters>(), null, CommandType.Text)).ReturnsAsync(1);
            // Act
            var result = await _bookRepository.DeleteBook(bookId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetBookById_ShouldReturnBook()
        {
            // Arrange
            int bookId = 1;
            var expectedBook = new BookModel { Id = bookId, Title = "Existing Book", Author = "Author", PublishedYear = 2020 };
            _dapperMock.Setup(d => d.QuerySingleAsync<BookModel>(It.IsAny<string>(), It.IsAny<DynamicParameters>(), null, null, CommandType.Text)).ReturnsAsync(expectedBook);
            // Act
            var result = await _bookRepository.GetBookById(bookId);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetBooks_ShouldReturnInMemoryBooks()
        {
            // Act
            var result = await _bookRepository.GetBooks();
            
            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task UpdateBook_ShouldReturnTrue_WhenBookIsUpdatedSuccessfully()
        {
            // Arrange
            var book = new BookModel { Id = 1, Title = "Updated Book", Author = "Updated Author", PublishedYear = 2024 };
            _dapperMock.Setup(d => d.ExecuteAsync(It.IsAny<string>(), It.IsAny<DynamicParameters>(), null, CommandType.Text)).ReturnsAsync(1);
            // Act
            var result = await _bookRepository.UpdateBook(book);

            // Assert
            Assert.True(result);
        }
    }
}
