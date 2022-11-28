using AutoMapper;
using Grade_Book_API.Entities;
using Grade_Book_API.Models;
using Grade_Book_API.Services;
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
        private readonly IGradeBookService _gradeBookService;

        public GradeBookController(IGradeBookService gradeBookService)
        {
            _gradeBookService = gradeBookService;
        }

        [HttpPost]
        public ActionResult AddStudent([FromBody] AddStudentDto dto)
        {
            var id = _gradeBookService.Create(dto);

            return Created($"/api/gradebook/students/{id}", null);
        }
        [HttpGet("{id}")]
        public ActionResult <IEnumerable<GradeDto>> GetAllGrades([FromRoute] int id)
        {
            var gradesDtos = _gradeBookService.GetGradesById(id);

            if(gradesDtos is null)
            {
                return NotFound();
            }

            return Ok(gradesDtos);
        }
    }
}
