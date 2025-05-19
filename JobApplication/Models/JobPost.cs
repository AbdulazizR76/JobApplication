using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobApplication.Models
{
    public class JobPost
    {

        public int Id { get; set; }

        public string JobTitle { get; set; }
        public string JobDescription { get; set; }

        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? CloseDate { get; set; }

        public bool IsDeleted { get; set; }

        public string Status { get; set; }

        [ForeignKey("User")]
        public string MadeByUserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<JobApplicationRequest> Applications { get; set; }
    }
}