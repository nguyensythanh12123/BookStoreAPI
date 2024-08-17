using BookStoreAPI.Model;
using BookStoreAPI.Services.Interfaces;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBook _book;

        public BookController(IBook book)
        {
            _book = book;
        }
        // GET: api/book
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                var books = await _book.GetBooks();
                return Ok(new ResultModel<List<BookModel>>("OK", "Success", books.Count(), books));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel("Bad Request", ex.Message));
            }
        }
        // GET: api/book/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GeBookById(int id)
        {
            try
            {
                if (id > 0)
                {
                    var book = await _book.GetBookById(id);
                    return Ok(new ResultModel<BookModel>("OK", "Success", book != null ? 1 : 0, book));
                }
                return BadRequest(new ErrorModel("Bad Request", "Invalid input data"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel("Bad Request", ex.Message));
            }
        }
        // Post: api/book
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] BookModel book)
        {
            try
            {
                var result = await _book.AddBook(book);
                return Ok(new ResultModel<bool>("OK", result ? "Suscess" : "Failed", result ? 1 : 0, result));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel("Bad Request", ex.Message));
            }
        }
        // Put: api/book/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookModel book)
        {
            try
            {
                var existingBook = await _book.GetBookById(id);
                if (existingBook != null)
                {
                    book.Id = id;
                    var result = await _book.UpdateBook(book);
                    return Ok(new ResultModel<bool>("OK", result ? "Suscess" : "Failed", result ? 1 : 0, result));
                }
                else
                {
                    return BadRequest(new ErrorModel("Bad Request", "No data found"));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel("Bad Request", ex.Message));
            }
        }
        // DELETE: api/books/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                var existingBook = await _book.GetBookById(id);
                if (existingBook != null)
                {
                    var result = await _book.DeleteBook(id);
                    return Ok(new ResultModel<bool>("OK", result ? "Suscess" : "Failed", result ? 1 : 0, result));
                }
                else
                {
                    return BadRequest(new ErrorModel("Bad Request", "No data found"));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
