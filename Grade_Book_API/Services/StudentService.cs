using AutoMapper;
using Grade_Book_API.Entities;
using Grade_Book_API.Exceptions;
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
        void DeleteStudent(int id);
        void UpdateStudent(int id, UpdateStudentDto dto);
    }
    public class StudentService : IStudentService
    {
        private readonly GradeBookDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<StudentService> _logger;

        public StudentService(GradeBookDbContext dbContext, IMapper mapper, ILogger<StudentService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public StudentDto GetStudentById(int id)
        {
            var student = _dbContext
                .Students
                .Include(s => s.Subjects)
                .FirstOrDefault(s => s.StudentId == id);

            if (student is null)
                throw new NotFoundException("Student not found");

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

        public void DeleteStudent(int id)
        {
            _logger.LogWarning($"Student with id: {id} DELETE action invoked");

            var student = _dbContext
               .Students
               .FirstOrDefault(s => s.StudentId == id);

            if (student is null)
                throw new NotFoundException("Student not found");

            _dbContext.Students.Remove(student);
            _dbContext.SaveChanges();
        }

        public void UpdateStudent(int id, UpdateStudentDto dto)
        {
            var student = _dbContext
               .Students
               .FirstOrDefault(s => s.StudentId == id);

            if (student is null)
                throw new NotFoundException("Student not found");

            if (!string.IsNullOrEmpty(dto.Surname))
                student.Surname = dto.Surname;

            if(!(dto.YearOfStudies == 0))
                student.YearOfStudies = dto.YearOfStudies;

            if(!string.IsNullOrEmpty(dto.DegreeCourse))
                student.DegreeCourse = dto.DegreeCourse;

            if(!string.IsNullOrEmpty(dto.ContactEmail))
                student.ContactEmail = dto.ContactEmail;

            _dbContext.SaveChanges();
        }
    }
}
