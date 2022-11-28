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
    public interface IGradeBookService
    {
        int Create(AddStudentDto dto);
        List<GradeDto> GetGradesById(int id);
    }

    public class GradeBookService : IGradeBookService
    {
        private readonly GradeBookDbContext _dbContext;
        private readonly IMapper _mapper;

        public GradeBookService(GradeBookDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<GradeDto> GetGradesById(int id)
        {
            var grades = _dbContext
                .Grades
                .Where(g => g.Student.StudentId == id)
                .Include(g => g.Subject)
                .ToList();

            if (grades is null)
            {
                return null;
            }

            var result = _mapper.Map<List<GradeDto>>(grades);
            return result;
        }

        public int Create(AddStudentDto dto)
        {
            var student = _mapper.Map<Student>(dto);
            _dbContext.Students.Add(student);
            _dbContext.SaveChanges();

            return student.StudentId;
        }
    }
}
