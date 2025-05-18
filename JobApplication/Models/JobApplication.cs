using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobApplication.Models
{
    public class JobApplicationRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public DateTime? InterviewDate { get; set; }

        public string Status { get; set; }

        //[ForeignKey("User")]
        //public string MadeByUserId { get; set; }
        //public virtual User User { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("JobPosts")]
        public int JobPostId { get; set; }
        public virtual JobPost JobPosts { get; set; }
    }
}