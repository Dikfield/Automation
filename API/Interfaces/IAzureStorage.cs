namespace API.Interfaces
{
    public interface IAzureStorage
    {
        Task<string> UploadFileAsync(IFormFile file, string fileName);
    }
}
