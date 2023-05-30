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
    public interface ISubjectService
    {
        List<SubjectWithGradesDto> GetAllSubjectsWithGrades(int id);
    }
    public class SubjectService : ISubjectService
    {
        private readonly GradeBookDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<StudentService> _logger;

        public SubjectService(GradeBookDbContext dbContext, IMapper mapper, ILogger<StudentService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }
        public List<SubjectWithGradesDto> GetAllSubjectsWithGrades(int id)
        {
            var student = _dbContext
                .Students
                .Include(s => s.Subjects)
                .Include(s => s.Grades)
                .FirstOrDefault(s => s.StudentId == id);

            if (student is null)
                throw new NotFoundException("Student not found");

            var subjects = student.Subjects;

            var subjectsWithGradesDto = _mapper.Map<List<SubjectWithGradesDto>>(subjects);
            return subjectsWithGradesDto;
        }
    }
}
