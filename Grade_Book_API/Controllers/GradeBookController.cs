using AutoMapper;
using Grade_Book_API.Entities;
using Grade_Book_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grade_Book_API.Controllers
{
    [Route("api/gradebook")]
    public class GradeBookController : ControllerBase
    {
        private readonly GradeBookDbContext _dbContext;
        private readonly IMapper _mapper;

        public GradeBookController(GradeBookDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult AddStudent([FromBody] AddStudentDto dto)
        {
            var student = _mapper.Map<Student>(dto);
            _dbContext.Students.Add(student);
            _dbContext.SaveChanges();

            return Created($"/api/gradebook/students/{student.StudentId}", null);
        }
        [HttpGet("{id}")]
        public ActionResult <IEnumerable<GradeDto>> GetAllGrades([FromRoute] int id)
        {
            var grades = _dbContext
                .Grades
                .Where(g => g.Student.StudentId == id)
                .Include(g => g.Subject)
                .ToList();

            var gradesDtos = _mapper.Map<List<GradeDto>>(grades);

            if(grades is null)
            {
                return NotFound();
            }

            return Ok(gradesDtos);
        }
    }
}
