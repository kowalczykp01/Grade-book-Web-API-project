using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grade_Book_API.Models
{
    public class UpdateStudentDto
    {
        [MaxLength(25)]
        public string? Surname { get; set; }
        public string? DegreeCourse { get; set; }
        public int YearOfStudies { get; set; }
        [EmailAddress]
        public string? ContactEmail { get; set; }
    }
}
