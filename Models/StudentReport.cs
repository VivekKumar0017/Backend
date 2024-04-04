using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class StudentReport:EntityBase
    {
        [Key]
        public int studentReportId { get; set; }
        public int AdmissionId { get; set; }

        public string Attendance { get; set; }

        public string Grade { get; set; }
        public string? FullName { get; set; }

       public Student? Student { get; set; }

        //public Course? Course { get; set; }
    }
}
