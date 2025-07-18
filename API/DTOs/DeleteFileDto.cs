using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class DeleteFileDto
    {
        public required string ClientId { get; set; }
        public int Id { get; set; }
    }
}
