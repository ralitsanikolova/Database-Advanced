using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace P01_StudentSystem.Data.Models
{
    class Student
    {
        public Student()
        {
            Homeworks = new HashSet<Homework>();
            Courses = new HashSet<StudentCourse>();
        }

        [Key]
        public string Studentld { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [StringLength(10, MinimumLength = 10)]
        public string PhoneNumber { get; set; }

        public DateTime RegistretedOn { get; set; }

        public DateTime? Birthday { get; set; }

        #region Relations
        public ICollection<Homework> Homeworks { get; set; }

        public ICollection<StudentCourse> Courses { get; set; }
        #endregion
    }
}
