using Grade_Book_API.Models;
using Grade_Book_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grade_Book_API.Controllers
{
    [Route("api/subjects")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }
        [HttpGet("grades/{studentId}")]
        [Authorize(Roles = "Student")]
        public ActionResult<SubjectWithGradesDto> GetAllWithGrades([FromRoute] int studentId)
        {
            List<SubjectWithGradesDto> subjects = _subjectService.GetAllSubjectsWithGrades(studentId);

            return Ok(subjects);
        }
    }
}
