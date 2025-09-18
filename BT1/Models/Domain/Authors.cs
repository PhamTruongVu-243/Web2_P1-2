using System.ComponentModel.DataAnnotations;

namespace BT1.Models.Domain
{
    public class Authors
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        //navigation Properties - One author has many book_author
        public List<Book_Author> Book_Authors { get; set; }
    }
}
