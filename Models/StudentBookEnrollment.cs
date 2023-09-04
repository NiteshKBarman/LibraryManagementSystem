namespace LibraryManagementSystem.Models
{
    public class StudentBookEnrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int BookId { get; set; }
        public DateTime Date { get; set; }
    }
}
