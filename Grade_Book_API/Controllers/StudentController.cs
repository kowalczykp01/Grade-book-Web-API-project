using AutoMapper;
using Grade_Book_API.Entities;
using Grade_Book_API.Models;
using Grade_Book_API.Services;
using Microsoft.AspNetCore.Authorization;
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
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        [HttpPost("register")]
        public ActionResult RegisterStudent([FromBody] RegisterStudentDto dto)
        {
            _studentService.RegisterStudent(dto);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody]LoginDto dto)
        {
            string token = _studentService.GenerateJwt(dto);

            return Ok(token);
        }
        [HttpPost]
        public ActionResult AddStudent([FromBody] AddStudentDto dto)
        {
            var id = _studentService.CreateStudent(dto);

            return Created($"/api/student/{id}", null);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Student")]
        public ActionResult<StudentDto> GetStudent([FromRoute] int id)
        {
            var studentDto = _studentService.GetStudentById(id);

            return Ok(studentDto);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteStudent([FromRoute] int id)
        {
            _studentService.DeleteStudent(id);

            return NoContent();
        }

        [HttpPut("{id}")]

        public ActionResult UpdateStudent([FromRoute] int id, [FromBody] UpdateStudentDto dto)
        {
            _studentService.UpdateStudent(id, dto);

            return Ok();
        }
    }
}
