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

                if (!_dbContext.Subjects.Any())
                {
                    var subjects = GetSubjects();
                    _dbContext.Subjects.AddRange(subjects);
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
                    Grades = new List<Grade>()
                    {
                        new Grade()
                        {
                            Value = 80,
                            SubjectId = 1002,
                            Description = "Final exam",
                            DateOfIssue = new DateTime(2022,10, 28)
                        },
                        new Grade()
                        {
                            Value = 95,
                            SubjectId = 1003,
                            Description = "Homework",
                            DateOfIssue = new DateTime(2022,10, 22)
                        }
                    }
                }
            };
            return students;
        }
        private IEnumerable<Subject> GetSubjects()
        {
            var subjects = new List<Subject>()
            {
                new Subject()
                {
                    Name = "Linear algebra"
                },
                new Subject()
                {
                    Name = "Numerical methods"
                }
            };
            return subjects;
        }
    }
}
