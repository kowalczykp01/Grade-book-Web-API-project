using Grade_Book_API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly IPasswordHasher<Student> _passwordHasher;

        public GradeBookSeeder(GradeBookDbContext dbContext, IPasswordHasher<Student> passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                var pendingMigrations = _dbContext.Database.GetPendingMigrations();

                if (pendingMigrations!=null && pendingMigrations.Any())
                {
                    _dbContext.Database.Migrate();
                }
                if (!_dbContext.Students.Any())
                {
                    var subjects = new List<Subject>
                    {
                        new Subject { Name = "Analiza matematyczna 2" },
                        new Subject { Name = "Podstawy kryptografii" },
                        new Subject { Name = "Metody numeryczne" }
                    };

                    var anotherSubjects = new List<Subject>
                    {
                        new Subject { Name = "RPiS"}
                    };

                    anotherSubjects.Add(subjects.ElementAt(1));

                    _dbContext.Subjects.AddRange(subjects);
                    _dbContext.Subjects.AddRange(anotherSubjects);
                    _dbContext.SaveChanges();

                    var student = new Student
                    {
                        FirstName = "Michał",
                        Surname = "Radzewicz",
                        DegreeCourse = "Informatyka",
                        YearOfStudies = 1,
                        ContactEmail = "s123456@sggw.edu.pl",
                        RoleId = 1,
                        Subjects = subjects
                    };

                    var hashedPassword = _passwordHasher.HashPassword(student, "haslo:mradzewicz:123");
                    student.PasswordHash = hashedPassword;

                    var anotherStudent = new Student
                    {
                        FirstName = "Paweł",
                        Surname = "Kowalczyk",
                        DegreeCourse = "Law",
                        YearOfStudies = 2,
                        ContactEmail = "s098765@sggw.edu.pl",
                        RoleId = 1,
                        Subjects = anotherSubjects
                    };

                    var anotherHashedPassword = _passwordHasher.HashPassword(anotherStudent, "haslo:pkowalczyk:123");
                    anotherStudent.PasswordHash = anotherHashedPassword;

                    var grades = new List<Grade>
                    {
                        new Grade { DateOfIssue = DateTime.Now, Description = "Wejściówka", Value = 90, Subject = subjects.ElementAt(0), Student = student },
                        new Grade { DateOfIssue = DateTime.Now, Description = "Projekt", Value = 80, Subject = subjects.ElementAt(0), Student = student },
                        new Grade { DateOfIssue = DateTime.Now, Description = "Kolokwium I", Value = 95, Subject = subjects.ElementAt(1), Student = student }
                    };

                    var anotherGrades = new List<Grade>
                    {
                        new Grade { DateOfIssue = DateTime.Now, Description = "Egzamin", Value = 80, Subject = anotherSubjects.ElementAt(0), Student = anotherStudent },
                        new Grade { DateOfIssue = DateTime.Now, Description = "Kolokwium", Value = 95, Subject = anotherSubjects.ElementAt(1), Student = anotherStudent }
                    };

                    student.Grades = grades;
                    anotherStudent.Grades = anotherGrades;

                    _dbContext.Students.Add(student);
                    _dbContext.Students.Add(anotherStudent);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }
            }           
        }
        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
                {
                    new Role()
                    {
                        Name = "Student"
                    },
                    new Role()
                    {
                        Name = "Teacher"
                    },
                    new Role()
                    {
                        Name = "Admin"
                    }
                };

            return roles;
        }
    }
}
