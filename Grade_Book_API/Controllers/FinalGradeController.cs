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
    [Route("api/finalgrade")]
    [ApiController]
    public class FinalGradeController : ControllerBase
    {
        private readonly IFinalGradeService _finalGradeService;

        public FinalGradeController(IFinalGradeService finalGradeServie)
        {
            _finalGradeService = finalGradeServie;
        }

        [HttpGet("{studentId}")]
        [Authorize(Roles = "Student")]
        public ActionResult<FinalGradeDto> GetAll([FromRoute] int studentId)
        {
            List<FinalGradeDto> finalGrades = _finalGradeService.GetAllFinalGrades(studentId);

            return Ok(finalGrades);
        }

        [HttpPost("{studentId}/{subjectId}")]
        public ActionResult Add([FromRoute]int studentId, [FromRoute]int subjectId, [FromBody]AddFinalGradeDto dto)
        {
            var id = _finalGradeService.AddFinalGrade(studentId, subjectId, dto);

            return Created($"/api/finalgrade/", null);
        }
    }
}
