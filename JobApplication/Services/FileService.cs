using JobApplication.Models;
using JobApplication.Models.ViewModels;
using JobApplication.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace JobApplication.Services
{
    public class FileService : IFileService
    {
        private readonly ApplicationDbContext _context;
        private readonly string _uploadRoot = HttpContext.Current.Server.MapPath("~/UploadedFiles");

        public FileService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AppFile> UploadAsync(HttpPostedFileBase file, string relatedEntityType, string relatedEntityId)
        {
            if (file == null || file.ContentLength == 0)
                return null;

            if (!Directory.Exists(_uploadRoot))
                Directory.CreateDirectory(_uploadRoot);

            string originalName = Path.GetFileName(file.FileName);
            string uniqueName = $"{Guid.NewGuid()}_{originalName}";
            string fullPath = Path.Combine(_uploadRoot, uniqueName);

            file.SaveAs(fullPath);

            var appFile = new AppFile
            {
                FileName = uniqueName,
                OriginalName = originalName,
                FilePath = fullPath,
                ContentType = file.ContentType,
                SizeInBytes = file.ContentLength,
                UploadedAt = DateTime.UtcNow,
                RelatedEntityId = relatedEntityId,
                RelatedEntityType = relatedEntityType
            };

            _context.AppFiles.Add(appFile);
            await _context.SaveChangesAsync();

            return appFile;
        }

        public Task<FileDownloadResult> DownloadAsync(int fileId)
        {
            var file = _context.AppFiles.Find(fileId);

            if (file == null || !System.IO.File.Exists(file.FilePath))
                return Task.FromResult<FileDownloadResult>(null);

            var result = new FileDownloadResult
            {
                FileBytes = System.IO.File.ReadAllBytes(file.FilePath),
                ContentType = file.ContentType,
                FileName = file.OriginalName
            };

            return Task.FromResult(result);
        }


        public async Task<bool> DeleteAsync(int fileId)
        {
            var file = await _context.AppFiles.FindAsync(fileId);
            if (file == null) return false;

            if (System.IO.File.Exists(file.FilePath))
                System.IO.File.Delete(file.FilePath);

            _context.AppFiles.Remove(file);
            await _context.SaveChangesAsync();
            return true;
        }

        public IEnumerable<AppFile> GetFiles(string relatedEntityType, string relatedEntityId)
        {
            return _context.AppFiles
                .Where(f => f.RelatedEntityType == relatedEntityType && f.RelatedEntityId == relatedEntityId)
                .ToList();
        }
    }
}