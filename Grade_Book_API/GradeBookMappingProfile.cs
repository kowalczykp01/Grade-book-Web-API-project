using AutoMapper;
using Grade_Book_API.Entities;
using Grade_Book_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grade_Book_API
{
    public class GradeBookMappingProfile : Profile
    {
        public GradeBookMappingProfile()
        {
            CreateMap<Grade, GradeDto>()
                .ForMember(m => m.Name, c => c.MapFrom(s => s.Subject.Name));

            CreateMap<AddStudentDto, Student>();

            CreateMap<Student, StudentDto>();
        }
    }
}
