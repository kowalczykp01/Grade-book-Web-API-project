namespace Grade_Book_API.Entities
{
    public class Grade
    {
        public int Id { get; set; }
        public DateTime DateOfIssue { get; set; }
        public string Description { get; set; }
        public int Value { get; set; }
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }             
    }
}
