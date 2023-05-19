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
    [Route("api/student")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost]
        public ActionResult AddStudent([FromBody] AddStudentDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = _studentService.CreateStudent(dto);

            return Created($"/api/student/{id}", null);
        }

        [HttpGet("{id}")]
        public ActionResult<StudentDto> GetStudent([FromRoute] int id)
        {
            var studentDto = _studentService.GetStudentById(id);

            if(studentDto is null)
                return NotFound();

            return Ok(studentDto);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteStudent([FromRoute] int id)
        {
            var isDeleted = _studentService.DeleteStudent(id);

            if (isDeleted)
                return NoContent();

            return NotFound();
        }

        [HttpPut("{id}")]

        public ActionResult UpdateStudent([FromRoute] int id, [FromBody] UpdateStudentDto dto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var isUpdated = _studentService.UpdateStudent(id, dto);

            if (!isUpdated) 
                return NotFound();

            return Ok();
        }
    }
}
