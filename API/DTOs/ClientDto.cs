namespace API.DTOs
{
    public class ClientDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Telephone { get; set; }
        public required string Cpf { get; set; }
        public required string Code { get; set; }
        public DateOnly? BirthDate { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public string? PassNumber { get; set; }
        public required string Token { get; set; } = string.Empty;
        public List<DocumentDto> Documents { get; set; } = [];
        public List<DestinyDto> Destinies { get; set; } = [];
    }
}
