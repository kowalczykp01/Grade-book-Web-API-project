using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grade_Book_API.Entities
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Grade> Grades { get; set; }
        public virtual Student Student { get; set; }
    }
}
