using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace P01_StudentSystem.Data.Models
{
    class Course
    {
        public Course()
        {
            Students = new HashSet<StudentCourse>();
            Homeworks = new HashSet<Homework>();
            Resources = new HashSet<Resource>();
        }

        [Key]
        public string Courseid { get; set; }

        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }

        #region Relations
        public ICollection<StudentCourse> Students { get; set; }
        public ICollection<Homework> Homeworks { get; set; }
        public ICollection<Resource> Resources { get; set; }
        #endregion
    }
}
