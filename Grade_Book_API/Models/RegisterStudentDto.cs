using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grade_Book_API.Models
{
    public class RegisterStudentDto
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string DegreeCourse { get; set; }
        public int YearOfStudies { get; set; }
        [Required]
        public string ContactEmail { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        public int RoleId { get; set; } = 1;
    }
}
