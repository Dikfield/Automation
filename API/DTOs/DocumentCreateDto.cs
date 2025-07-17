namespace API.DTOs
{
    public class DocumentCreateDto
    {
        public required string ClientId { get; set; }
        public required IFormFile File { get; set; }
    }
}
