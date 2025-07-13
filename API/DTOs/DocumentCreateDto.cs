namespace API.DTOs
{
    public class DocumentCreateDto
    {
        public required string ClientCpf { get; set; }
        public required string FileName { get; set; }
        public required string ContentType { get; set; }
        public required IFormFile File { get; set; }
    }
}
