using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class Document
    {
        public int Id { get; set; }
        public required string FileName { get; set; }
        public required string ContentType { get; set; }
        public required string Url { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        public Client Client { get; set; } = null!;
        public string ClientId { get; set; } = null!;
    }
}
