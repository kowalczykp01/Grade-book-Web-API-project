using Grade_Book_API.Entities;
using Microsoft.AspNetCore.Mvc;
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

        public GradeBookController(GradeBookDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("{id}")]
        public ActionResult <IEnumerable<Grade>> GetAllGrades([FromRoute] int id)
        {
            var grades = _dbContext
                .Grades
                .Where(g => g.Student.StudentId == id)
                .ToList();

            if(grades is null)
            {
                return NotFound();
            }

            return Ok(grades);
        }
    }
}
