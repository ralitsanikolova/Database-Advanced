namespace P01_StudentSystem.Data.Models
{
    class StudentCourse
    {
        public string StudentId { get; set; }
        public string CourseId { get; set; }

        #region Relations
        public Course Course { get; set; }
        public Student Student { get; set; }
        #endregion 
    }
}
