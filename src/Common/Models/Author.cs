namespace Common.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset Birthday { get; set; }
        public string Note { get; set; }
    }
}
