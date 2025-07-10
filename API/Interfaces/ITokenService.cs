namespace API.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(string email, string id);
    }
}
