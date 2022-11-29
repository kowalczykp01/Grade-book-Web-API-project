﻿using AutoMapper;
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
        StudentDto GetStudentById(int id);
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
        public StudentDto GetStudentById(int id)
        {
            var student = _dbContext
                .Students
                .Include(s => s.Grades)
                .FirstOrDefault(s => s.StudentId == id);

            if (student is null)
            {
                return null;
            }

            var result = _mapper.Map<StudentDto>(student);
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