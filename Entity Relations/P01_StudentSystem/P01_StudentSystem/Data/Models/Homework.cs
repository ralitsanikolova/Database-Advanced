using System;
using System.ComponentModel.DataAnnotations;

namespace P01_StudentSystem.Data.Models
{
    class Homework
    {
        [Key]
        public string HomeworkId { get; set; }
        public string Content { get; set; }
        public ContentType ContentType { get; set; }
        public DateTime SubmissionTime { get; set; }
        public string StudentId { get; set; }
        public string CourseId { get; set; }

        #region Relations
        public Course Course { get; set; }
        public Student Student { get; set; }
        #endregion
    }
}
