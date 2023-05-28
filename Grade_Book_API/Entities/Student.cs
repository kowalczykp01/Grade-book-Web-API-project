﻿namespace Grade_Book_API.Entities
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string DegreeCourse { get; set; }
        public int YearOfStudies { get; set; }
        public string ContactEmail { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
        public virtual ICollection<FinalGrade> FinalGrades { get; set; }
    }
}
