using JobApplication.Models;
using JobApplication.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace JobApplication.Services.Interfaces
{
    public interface IFileService
    {
        Task<AppFile> UploadAsync(HttpPostedFileBase file, string relatedEntityType, string relatedEntityId);
        //Task<byte[]> DownloadAsync(int fileId, out string contentType, out string downloadFileName);
        Task<FileDownloadResult> DownloadAsync(int fileId);

        Task<bool> DeleteAsync(int fileId);
        IEnumerable<AppFile> GetFiles(string relatedEntityType, string relatedEntityId);
    }
}
