using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public enum AdmissionStatus
    {
        Accepted,
        Rejected
    }

    public class Admission : EntityBase
    {
        [Key]
        public int AdmissionId { get; set; }
        public int StudentId { get; set; }
        public virtual Student Student { get; set; } // Make virtual
        public int CollegeId { get; set; }
        public virtual College College { get; set; } // Make virtual
        
        public AdmissionStatus Status { get; set; }


    }
}
