using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grade_Book_API.Models
{
    public class LoginDto
    {
        public string ContactEmail { get; set; }
        public string Password { get; set; }
    }
}
