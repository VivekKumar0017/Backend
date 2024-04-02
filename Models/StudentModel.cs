using System.ComponentModel.DataAnnotations;

using static System.Net.Mime.MediaTypeNames;

namespace Backend.Models
{
    public class Student : EntityBase
    {
        [Key]
        public int AdmissionId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }




        public int collegeUniqueId { get; set; }

        public College? College { get; set; }

       /* public virtual ICollection<Course>? Courses { get; set; }*/

        public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();


    }

}
