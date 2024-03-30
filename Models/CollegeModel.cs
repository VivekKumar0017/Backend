using System.ComponentModel.DataAnnotations;


namespace Backend.Models
{

    public class EntityBase { }


    public class College : EntityBase
    {
        [Key]
        public int CollegeId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        // Make Courses and Admissions collections nullable
        public virtual ICollection<Course>? Courses { get; set; }

        // Navigation property for related admissions
        public virtual ICollection<Admission>? Admissions { get; set; }
    }

}
