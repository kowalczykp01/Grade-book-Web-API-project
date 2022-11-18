using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grade_Book_API.Models
{
    public class AddStudentDto
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string DegreeCourse { get; set; }
        public int YearOfStudies { get; set; }
        public string ContactEmail { get; set; }
    }
}
