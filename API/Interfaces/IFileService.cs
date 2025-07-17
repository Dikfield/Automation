using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;

namespace API.Interfaces
{
    public interface IFileService
    {
        Task<UploadResult> UploadAsync(IFormFile file);
        Task<DeletionResult> DeleteFileAsync(string publicId);
        Task<byte[]> DownloadFileAsync(string publicId, string resourceType = "raw");
        public string GenerateDownloadUrl(string publicId, string resourceType = "raw");
    }
}
