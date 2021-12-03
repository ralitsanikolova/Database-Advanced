using System.ComponentModel.DataAnnotations;

namespace P01_StudentSystem.Data.Models
{
    class Resource
    {
        [Key]
        public string ResourseId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public string URL { get; set; }
        public string CourseId { get; set; }
        public ResourseType ResourseType { get; set; }

        #region Relations
        public Course Course { get; set; }
        #endregion 
    }
}
