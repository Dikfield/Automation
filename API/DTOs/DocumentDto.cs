using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class DocumentDto
    {
        public required string ClientCpf { get; set; }
        public required string FileName { get; set; }
        public required string ContentType { get; set; }
        public required IFormFile File { get; set; }
    }
}
