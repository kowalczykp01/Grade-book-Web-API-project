using Grade_Book_API.Entities;
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

        public GradeBookSeeder(GradeBookDbContext dbContext)
        {
            _dbContext = dbContext;
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
                        new Subject { Name = "Mathematics" },
                        new Subject { Name = "Physics" },
                        new Subject { Name = "Chemistry" }
                    };

                    _dbContext.Subjects.AddRange(subjects);
                    _dbContext.SaveChanges();

                    var student = new Student
                    {
                        FirstName = "John",
                        Surname = "Doe",
                        DegreeCourse = "Computer Science",
                        YearOfStudies = 3,
                        ContactEmail = "john.doe@example.com",
                        PasswordHash = "zahaszowanehaslo",
                        RoleId = 1,
                        Subjects = subjects
                    };

                    var grades = new List<Grade>
                    {
                        new Grade { DateOfIssue = DateTime.Now, Description = "Grade 1", Value = 90, Subject = subjects.ElementAt(0), Student = student },
                        new Grade { DateOfIssue = DateTime.Now, Description = "Grade 2", Value = 80, Subject = subjects.ElementAt(0), Student = student },
                        new Grade { DateOfIssue = DateTime.Now, Description = "Grade 3", Value = 95, Subject = subjects.ElementAt(1), Student = student }
                    };

                    student.Grades = grades;

                    _dbContext.Students.Add(student);
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
