using System.ComponentModel.DataAnnotations;


namespace Backend.Models
{

    public class EntityBase { }


    public class College : EntityBase
    {
        [Key]
        public int collegeUniqueId { get; set; }
        public int CollegeId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        // Navigation property
        public virtual ICollection<Course> Courses { get; set; }=new List<Course>();

        public virtual ICollection<Student> Students { get; set; } = new List<Student>();



    }

}
