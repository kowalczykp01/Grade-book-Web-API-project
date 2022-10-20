using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grade_Book_API.Entities
{
    public class Grade
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public virtual Subject Subject { get; set; }
        public int StudentId { get; set; }
    }
}
