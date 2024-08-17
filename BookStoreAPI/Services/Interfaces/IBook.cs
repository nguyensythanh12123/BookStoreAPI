using BookStoreAPI.Model;

namespace BookStoreAPI.Services.Interfaces
{
    public interface IBook
    {
        Task<List<BookModel>> GetBooks();
        Task<BookModel> GetBookById(int id);
        Task<bool> AddBook(BookModel book);
        Task<bool> UpdateBook(BookModel book);
        Task<bool> DeleteBook(int id);
    }
}
