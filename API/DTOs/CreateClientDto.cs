using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class CreateClientDto
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Email { get; set; }

        [Required]
        [MinLength(10)]
        public required string Cpf { get; set; }

        [Required]
        [MinLength(10)]
        public required string Telephone { get; set; }
        public DateOnly? BirthDate { get; set; }
        public string? PassNumber { get; set; }
    }
}
