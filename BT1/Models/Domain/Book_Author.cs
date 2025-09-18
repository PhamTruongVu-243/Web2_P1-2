namespace BT1.Models.Domain
{
    public class Book_Author
    {
        public int Id { get; set; } // Primary key
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int AuthorId { get; set; }
        public Authors Authors { get; set; }
    }
}
