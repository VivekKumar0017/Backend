namespace Backend.Models
{
    public class StudentReport:EntityBase
    {
        public int AdmissionId { get; set; }

        public string Attendance { get; set; }

        public string Grade { get; set; }

       public Student? Student { get; set; }
    }
}
