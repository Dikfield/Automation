using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IAzureStorage
    {
        Task<string> UploadFileAsync(IFormFile file, string fileName);
    }
}
