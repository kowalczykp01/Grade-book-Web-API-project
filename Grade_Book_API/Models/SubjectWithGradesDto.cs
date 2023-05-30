using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grade_Book_API.Models
{
    public class SubjectWithGradesDto
    {
        public string Name { get; set; }
        public List<GradeDto> Grades { get; set; }
    }
}
