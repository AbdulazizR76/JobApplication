using System;

namespace JobApplication.Models
{
    public class AppFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string OriginalName { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }
        public long SizeInBytes { get; set; }
        public DateTime UploadedAt { get; set; }

        // Optional reference to a parent
        public string RelatedEntityId { get; set; }
        public string RelatedEntityType { get; set; } // e.g., "User", "Project", "Task"
        public string FileCategory { get; set; } // e.g., "CV", "ProfilePicture", "Contract"

    }
}