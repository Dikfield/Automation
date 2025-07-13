using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class DestinyDto
    {
        public int Id { get; set; }
        public required string ClientID { get; set; }
        public required string Country { get; set; }
        public required string City { get; set; }
        public DateTime TravelDate { get; set; }
        public int Persons { get; set; }
        public int Tours { get; set; }
    }
}
