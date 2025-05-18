namespace JobApplication.Models.ViewModels
{
    public class FileDownloadResult
    {
        public byte[] FileBytes { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
    }

}