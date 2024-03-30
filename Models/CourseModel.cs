using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Course : EntityBase
    {
        [Key]
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public int CollegeId { get; set; } // Foreign key to College
        public virtual College College { get; set; } // Make virtual
    }
}
