using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EnglishCourses.Domain.Entities.Teacher
{
    public class TeacherDbTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public byte[] ProfilePicture { get; set; }
        public int TotalStudents { get; set; }
        public DateTime JoinDate { get; set; }
    }
}