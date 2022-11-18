﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grade_Book_API.Models
{
    public class GradeDto
    {
        public int Id { get; set; }
        public DateTime DateOfIssue { get; set; }
        public string Description { get; set; }
        public int Value { get; set; }
        public int SubjectId { get; set; }
        public string Name { get; set; }
    }
}