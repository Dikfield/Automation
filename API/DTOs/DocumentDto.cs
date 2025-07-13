using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class DocumentDto
    {
        public int Id { get; set; }
        public required string ClientID { get; set; }
        public required string FileName { get; set; }
        public required string ContentType { get; set; }
        public required string Url { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}
