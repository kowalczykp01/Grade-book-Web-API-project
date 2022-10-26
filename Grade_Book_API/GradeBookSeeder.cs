using Grade_Book_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grade_Book_API
{
    public class GradeBookSeeder
    {
        private readonly GradeBookDbContext _dbContext;

        public GradeBookSeeder(GradeBookDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Students.Any())
                {
                    var students = GetStudents();
                    _dbContext.Students.AddRange(students);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Student> GetStudents()
        {
            var students = new List<Student>()
            {
                new Student()
                {
                    FirstName = "Paweł",
                    Surname = "Kowalczyk",
                    DegreeCourse = "Computer Science",
                    YearOfStudies = 3,
                    ContactEmail = "pawelkowalczyk@email.com",
                    Subjects = new List<Subject>()
                    {
                        new Subject()
                        {
                            Name = "Numerical methods",
                            Grades = new List<Grade>()
                            {
                                new Grade()
                                {
                                    Description = "Exam",
                                    Value = 10
                                },
                                new Grade()
                                {
                                    Description = "Test",
                                    Value = 57
                                }
                            }
                        }               
                    }
                }
            };
            return students;
        }
    }
}
