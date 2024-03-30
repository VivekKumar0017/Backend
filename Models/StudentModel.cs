using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace Backend.Models
{
    public class Student : EntityBase
    {
        [Key]
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int CollegeId { get; set; }
        public virtual College College { get; set; } // Make virtual

        public ICollection<Admission> Admissions { get; set; }

    }

}
