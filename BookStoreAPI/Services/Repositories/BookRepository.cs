using BookStoreAPI.Model;
using BookStoreAPI.Services.Dapper;
using BookStoreAPI.Services.Interfaces;
using Dapper;
using System.Transactions;

namespace BookStoreAPI.Services.Repositories
{
    public class BookRepository : IBook
    {
        private readonly IDapper _dapper;
        private readonly List<BookModel> _inMemoryBooks = new List<BookModel>(); // In-memory list
        public BookRepository(IDapper dapper, List<BookModel> inMemoryBooks)
        {
            _dapper = dapper;
            _inMemoryBooks.Add(new BookModel { Id = 1, Title = "The Pragmatic Programmer ", Author = "Andrew Hunt và David Thomas", PublishedYear = 1999 });
            _inMemoryBooks.Add(new BookModel { Id = 2, Title = "Clean Code: A Handbook of Agile Software Craftsmanship", Author = "Robert C. Martin", PublishedYear = 2008 });
        }

        public async Task<bool> AddBook(BookModel book)
        {
            try
            {
                #region Add from in-memory list
                //book.Id = _inMemoryBooks.Max(b => b.Id) + 1;
                //_inMemoryBooks.Add(book);
                //return true;
                #endregion
                DynamicParameters param = new DynamicParameters();
                param.Add("@Title", book.Title);
                param.Add("@Author", book.Author);
                param.Add("@PublishedYear", book.PublishedYear);
                var sql = "INSERT INTO Books (Title, Author, PublishedYear) VALUES (@Title, @Author, @PublishedYear)";
                return await _dapper.ExecuteAsync(sql, param) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteBook(int id)
        {
            try
            {
                #region Delete from in-memory list
                //var book = _inMemoryBooks.FirstOrDefault(b => b.Id == id);
                //if (book == null)
                //{
                //    throw new Exception("Not Found");
                //}

                //_inMemoryBooks.Remove(book);
                #endregion

                DynamicParameters param = new();
                param.Add("@id", id);
                var sql = "Delete top (1) from Books WHERE Id = @id";
                return await _dapper.ExecuteAsync(sql, param) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BookModel> GetBookById(int id)
        {
            try
            {
                // Search in-memory list first
                //var book = _inMemoryBooks.FirstOrDefault(b => b.Id == id);
                // To use database later:
                DynamicParameters param = new DynamicParameters();
                param.Add("@id", id);
                var book = await _dapper.QuerySingleAsync<BookModel>("SELECT top (1) Id, Title, Author, PublishedYear  FROM Books (Nolock) WHERE Id = @Id", param);
                return book;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<BookModel>> GetBooks()
        {
            try
            {
                #region To use database later:
                //var books = await _dapper.QueryAsync<BookModel>("SELECT * FROM Books(nolock)", null);
                //return books.ToList();
                #endregion
                return _inMemoryBooks;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateBook(BookModel book)
        {
            try
            {
                #region Update in-memory list
                //var existingBook = _inMemoryBooks.FirstOrDefault(b => b.Id == book.Id);
                //if (existingBook == null)
                //{
                //    return fasle;
                //}

                //existingBook.Title = book.Title;
                //existingBook.Author = book.Author;
                //existingBook.PublishedYear = book.PublishedYear;
                #endregion
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", book.Id);
                param.Add("@Title", book.Title);
                param.Add("@Author", book.Author);
                param.Add("@PublishedYear", book.PublishedYear);
                var sql = "UPDATE top (1) Books SET Title = @Title, Author = @Author, PublishedYear = @PublishedYear WHERE Id = @Id";
                return await _dapper.ExecuteAsync(sql, param) > 0;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
