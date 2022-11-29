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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = _gradeBookService.Create(dto);

            return Created($"/api/gradebook/students/{id}", null);
        }
        [HttpGet("{id}")]
        public ActionResult<StudentDto> GetStudent([FromRoute] int id)
        {
            var studentDto = _gradeBookService.GetStudentById(id);

            if(studentDto is null)
            {
                return NotFound();
            }

            return Ok(studentDto);
        }
    }
}
