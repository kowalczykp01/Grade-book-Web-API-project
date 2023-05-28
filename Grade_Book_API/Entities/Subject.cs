namespace Grade_Book_API.Entities
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<FinalGrade> FinalGrades { get; set; }
    }
}
