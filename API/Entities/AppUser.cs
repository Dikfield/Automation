using System.Text.Json.Serialization;

namespace API.Entities;

public class AppUser
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string DisplayName { get; set; }
    public required string Email { get; set; }
    [JsonIgnore]
    public required byte[] PasswordHash { get; set; }
    [JsonIgnore]
    public required byte[] PasswordSalt { get; set; }
}
