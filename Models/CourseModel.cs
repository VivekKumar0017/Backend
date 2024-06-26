﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Course : EntityBase
    {
        [Key]
        public int courseUniqueId { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        // Foreign key
        public int collegeUniqueId { get; set; }

        public string courseFees { get; set; }

        public string CourseDuration { get; set; }   

        public string ElegblityCriteria { get; set; }
        // Navigation property
        
        public College? College { get; set; }

        public virtual ICollection<Student>? Students { get; set; } 

    }
}

