using AutoMapper;
using Grade_Book_API.Entities;
using Grade_Book_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grade_Book_API.Services
{
    public interface IStudentService
    {
        int CreateStudent(AddStudentDto dto);
        StudentDto GetStudentById(int id);
        bool DeleteStudent(int id);
        bool UpdateStudent(int id, UpdateStudentDto dto);
    }
    public class StudentService : IStudentService
    {
        private readonly GradeBookDbContext _dbContext;
        private readonly IMapper _mapper;

        public StudentService(GradeBookDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public StudentDto GetStudentById(int id)
        {
            var student = _dbContext
                .Students
                .Include(s => s.Grades)
                .Include(s => s.Subjects)
                .FirstOrDefault(s => s.StudentId == id);

            if (student is null) return null;

            var result = _mapper.Map<StudentDto>(student);
            return result;
        }

        public int CreateStudent(AddStudentDto dto)
        {
            var student = _mapper.Map<Student>(dto);
            _dbContext.Students.Add(student);
            _dbContext.SaveChanges();

            return student.StudentId;
        }

        public bool DeleteStudent(int id)
        {
            var student = _dbContext
               .Students
               .FirstOrDefault(s => s.StudentId == id);

            if (student is null) return false;

            _dbContext.Students.Remove(student);
            _dbContext.SaveChanges();

            return true;
        }

        public bool UpdateStudent(int id, UpdateStudentDto dto)
        {
            var student = _dbContext
               .Students
               .FirstOrDefault(s => s.StudentId == id);

            if (student is null) return false;

            student.Surname = dto.Surname;
            student.YearOfStudies = dto.YearOfStudies;
            student.DegreeCourse = dto.DegreeCourse;
            student.ContactEmail = dto.ContactEmail;

            _dbContext.SaveChanges();

            return true;
        }
    }
}
