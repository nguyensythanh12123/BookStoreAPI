using BookStoreAPI.Controllers;
using BookStoreAPI.Model;
using BookStoreAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreAPI.Test.Controllers.Test
{
    public class BooksControllerTests
    {
        private readonly BookController _bookController;
        private readonly Mock<IBook> _mockBookService;

        public BooksControllerTests()
        {
            _mockBookService = new Mock<IBook>();
            _bookController = new BookController(_mockBookService.Object);
        }
        [Fact]
        public async Task GetAllBooks_ReturnsOkResult_WithResultModel()
        {
            // Arrange
            var books = new List<BookModel>
            {
                new BookModel { Id = 1, Title = "Book 1", Author = "Author 1", PublishedYear = 2021 },
                new BookModel { Id = 2, Title = "Book 2", Author = "Author 2", PublishedYear = 2022 }
            };

            _mockBookService.Setup(service => service.GetBooks()).ReturnsAsync(books);

            // Act
            var result = await _bookController.GetAllBooks();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultModel = Assert.IsType<ResultModel<List<BookModel>>>(okResult.Value);

            // Assert
            Assert.Equal("OK", resultModel.Status);
            Assert.Equal("Success", resultModel.Description);
            Assert.Equal(books.Count, resultModel.Amount);
            Assert.Equal(books, resultModel.Data);
        }

        [Fact]
        public async Task GetBookById_ReturnsOkResult_WithResultModel()
        {
            // Arrange
            var book = new BookModel { Id = 1, Title = "Book 1", Author = "Author 1", PublishedYear = 2021 };
            _mockBookService.Setup(service => service.GetBookById(1)).ReturnsAsync(book);

            // Act
            var result = await _bookController.GeBookById(1);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultModel = Assert.IsType<ResultModel<BookModel>>(okResult.Value);

            // Assert
            Assert.Equal("OK", resultModel.Status);
            Assert.Equal("Success", resultModel.Description);
            Assert.Equal(1, resultModel.Amount);
            Assert.Equal(book, resultModel.Data);
        }

        [Fact]
        public async Task AddBook_ReturnsOkResult_WithResultModel()
        {
            // Arrange
            var book = new BookModel { Id = 1, Title = "Book 1", Author = "Author 1", PublishedYear = 2021 };
            _mockBookService.Setup(service => service.AddBook(book)).ReturnsAsync(true);

            // Act
            var result = await _bookController.AddBook(book);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultModel = Assert.IsType<ResultModel<bool>>(okResult.Value);

            // Assert
            Assert.Equal("OK", resultModel.Status);
            Assert.Equal("Suscess", resultModel.Description); 
            Assert.Equal(1, resultModel.Amount);
            Assert.True(resultModel.Data);
        }

        [Fact]
        public async Task UpdateBook_ReturnsOkResult_WithResultModel()
        {
            // Arrange
            var book = new BookModel { Id = 1, Title = "Updated Book", Author = "Author", PublishedYear = 2022 };
            _mockBookService.Setup(service => service.GetBookById(1)).ReturnsAsync(book);
            _mockBookService.Setup(service => service.UpdateBook(book)).ReturnsAsync(true);

            // Act
            var result = await _bookController.UpdateBook(1, book);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultModel = Assert.IsType<ResultModel<bool>>(okResult.Value);

            // Assert
            Assert.Equal("OK", resultModel.Status);
            Assert.Equal("Suscess", resultModel.Description); 
            Assert.Equal(1, resultModel.Amount);
            Assert.True(resultModel.Data);
        }

        [Fact]
        public async Task DeleteBook_ReturnsOkResult_WithResultModel()
        {
            // Arrange
            var book = new BookModel { Id = 1, Title = "Book to Delete", Author = "Author", PublishedYear = 2021 };
            _mockBookService.Setup(service => service.GetBookById(1)).ReturnsAsync(book);
            _mockBookService.Setup(service => service.DeleteBook(1)).ReturnsAsync(true);

            // Act
            var result = await _bookController.DeleteBook(1);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultModel = Assert.IsType<ResultModel<bool>>(okResult.Value);

            // Assert
            Assert.Equal("OK", resultModel.Status);
            Assert.Equal("Suscess", resultModel.Description); 
            Assert.Equal(1, resultModel.Amount);
            Assert.True(resultModel.Data);
        }
    }
}
