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
    public interface IFinalGradeService
    {
        List<FinalGradeDto> GetAllFinalGrades(int id);
        int AddFinalGrade(int studentId, int subjectId, AddFinalGradeDto dto);
    }
    public class FinalGradeService : IFinalGradeService
    {
        private readonly GradeBookDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<StudentService> _logger;

        public FinalGradeService(GradeBookDbContext dbContext, IMapper mapper, ILogger<StudentService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }
        public List<FinalGradeDto> GetAllFinalGrades(int id)
        {
            var student = _dbContext
                .Students
                .Include(s => s.FinalGrades)
                .Include(s => s.Subjects)
                .FirstOrDefault(s => s.StudentId == id);

            if (student is null)
                throw new NotFoundException("Student not found");

            var finalGrades = student.FinalGrades;

            var finalGradesDto = _mapper.Map<List<FinalGradeDto>>(finalGrades);
            return finalGradesDto;
        }

        public int AddFinalGrade(int studentId, int subjectId, AddFinalGradeDto dto)
        {
            var student = _dbContext.Students
                .Include(s => s.Subjects)
                .Include(s => s.FinalGrades)
                .FirstOrDefault(s => s.StudentId == studentId);

            if (student is null)
                throw new NotFoundException("Student not found");

            var subject = student.Subjects
                .FirstOrDefault(s => s.Id == subjectId);

            if (subject is null)
                throw new NotFoundException("Subject not found");

            var finalGrade = _mapper.Map<FinalGrade>(dto);
            finalGrade.Subject = subject;

            student.FinalGrades.Add(finalGrade);
            _dbContext.SaveChanges();

            return finalGrade.Id;
        }
    }
}
