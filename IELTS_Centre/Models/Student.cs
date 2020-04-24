using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IELTS_Centre.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string FatherName { get; set; }
        public int CourseID { get; set; }
        public int TeacherID { get; set; }
        [ForeignKey("CourseID")]
        public Course Course { get; set; }
        [ForeignKey("TeacherID")]
        public Teacher Teacher { get; set; }
    }
}
