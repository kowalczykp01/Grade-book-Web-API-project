using AutoMapper;
using Grade_Book_API.Entities;
using Grade_Book_API.Exceptions;
using Grade_Book_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Grade_Book_API.Services
{
    public interface IStudentService
    {
        int CreateStudent(AddStudentDto dto);
        StudentDto GetStudentById(int id);
        void DeleteStudent(int id);
        void UpdateStudent(int id, UpdateStudentDto dto);
        void RegisterStudent(RegisterStudentDto dto);
        string GenerateJwt(LoginDto dto);
    }
    public class StudentService : IStudentService
    {
        private readonly GradeBookDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<StudentService> _logger;
        private readonly IPasswordHasher<Student> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public StudentService(GradeBookDbContext dbContext, IMapper mapper, ILogger<StudentService> logger, IPasswordHasher<Student> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }
        public void RegisterStudent(RegisterStudentDto dto)
        {
            var newStudent = new Student()
            {
                FirstName = dto.FirstName,
                Surname = dto.Surname,
                DegreeCourse = dto.DegreeCourse,
                YearOfStudies = dto.YearOfStudies,
                ContactEmail = dto.ContactEmail,
                RoleId = dto.RoleId
            };
            var hashedPassword = _passwordHasher.HashPassword(newStudent, dto.Password);
            newStudent.PasswordHash = hashedPassword;
            _dbContext.Students.Add(newStudent);
            _dbContext.SaveChanges();
        }

        public StudentDto GetStudentById(int id)
        {
            var student = _dbContext
                .Students
                .Include(s => s.Subjects)
                .FirstOrDefault(s => s.StudentId == id);

            if (student is null)
                throw new NotFoundException("Student not found");

            var result = _mapper.Map<StudentDto>(student);
            return result;
        }

        public int CreateStudent(AddStudentDto dto)
        {
            var student = _mapper.Map<Student>(dto);
            _dbContext.Students.Add(student);
            _dbContext.SaveChanges();

            return student.StudentId;
        }

        public void DeleteStudent(int id)
        {
            _logger.LogWarning($"Student with id: {id} DELETE action invoked");

            var student = _dbContext
               .Students
               .FirstOrDefault(s => s.StudentId == id);

            if (student is null)
                throw new NotFoundException("Student not found");

            _dbContext.Students.Remove(student);
            _dbContext.SaveChanges();
        }

        public void UpdateStudent(int id, UpdateStudentDto dto)
        {
            var student = _dbContext
               .Students
               .FirstOrDefault(s => s.StudentId == id);

            if (student is null)
                throw new NotFoundException("Student not found");

            if (!string.IsNullOrEmpty(dto.Surname))
                student.Surname = dto.Surname;

            if(!(dto.YearOfStudies == 0))
                student.YearOfStudies = dto.YearOfStudies;

            if(!string.IsNullOrEmpty(dto.DegreeCourse))
                student.DegreeCourse = dto.DegreeCourse;

            if(!string.IsNullOrEmpty(dto.ContactEmail))
                student.ContactEmail = dto.ContactEmail;

            _dbContext.SaveChanges();
        }

        public string GenerateJwt(LoginDto dto)
        {
            var student = _dbContext.
                Students.
                Include(s => s.Role)
                .FirstOrDefault(s => s.ContactEmail == dto.ContactEmail);

            if (student is null)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var result = _passwordHasher.VerifyHashedPassword(student, student.PasswordHash, dto.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, student.StudentId.ToString()),
                new Claim(ClaimTypes.Name, $"{student.FirstName} {student.Surname}"),
                new Claim(ClaimTypes.Role, $"{student.Role.Name}"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
