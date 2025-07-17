using API.Helpers;
using API.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace API.Services
{
    public class FileService : IFileService
    {
        private readonly Cloudinary _cloudinary;
        private readonly string _cloudName;

        public FileService(IOptions<CloudinarySettings> config)
        {
            _cloudName = config.Value.CloudName;
            var account = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );
            _cloudinary = new Cloudinary(account);
        }

        public async Task<DeletionResult> DeleteFileAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);

            return await _cloudinary.DestroyAsync(deleteParams);
        }

        public async Task<UploadResult> UploadAsync(IFormFile file)
        {
            var isImage = file.ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase);
            if (isImage)
            {
                var uploadResult = new ImageUploadResult();

                if (file.Length > 0)
                {
                    await using var stream = file.OpenReadStream();
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, stream),
                        Transformation = new Transformation().Height(500).Width(500).Crop("fill"),
                        Folder = "Vivaturismo",
                    };

                    uploadResult = await _cloudinary.UploadAsync(uploadParams);
                }

                return uploadResult;
            }
            else
            {
                using var stream = file.OpenReadStream();

                var uploadParams = new RawUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = "Vivaturismo",
                };
                return await _cloudinary.UploadAsync(uploadParams);
            }
        }

        public async Task<byte[]> DownloadFileAsync(string publicId, string resourceType = "raw")
        {
            var url = GenerateDownloadUrl(publicId, resourceType);

            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                throw new InvalidOperationException("URL inválida para download: " + url);

            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Erro ao baixar arquivo: {response.StatusCode}");

            return await response.Content.ReadAsByteArrayAsync();
        }

        public string GenerateDownloadUrl(string publicId, string resourceType = "raw")
        {
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();

            var parameters = new SortedDictionary<string, object>
            {
                { "public_id", publicId },
                { "resource_type", resourceType },
                { "timestamp", timestamp },
            };

            var signature = _cloudinary.Api.SignParameters(parameters);

            var url =
                $"https://res.cloudinary.com/{_cloudName}/"
                + $"{resourceType}/upload/{publicId}"
                + $"?timestamp={timestamp}&signature={signature}&api_key={_cloudinary.Api.Account.ApiKey}";

            return url;
        }
    }
}
