namespace Backend.Models
{
    public class StudentCourse
    {
        public int AdmissionId { get; set; }
        public Student? Student { get; set; }
        public int courseUniqueId { get; set; }
        public Course? Course { get; set; }
    }
}
    