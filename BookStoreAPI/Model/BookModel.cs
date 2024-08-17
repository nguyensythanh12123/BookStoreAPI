using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Model
{
    public class BookModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Author is required.")]
        public string Author { get; set; }
        [Range(1450, 2024, ErrorMessage = "Published year must be between 1450 and 2024.")]
        public int PublishedYear { get; set; }
    }
}
