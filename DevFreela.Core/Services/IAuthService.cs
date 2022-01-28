namespace DevFreela.Core.Services
{
    public interface IAuthService
    {
        string GenerateJwtToken(string email, string role); // Role poderia ser um enum, uma lista e entre outros...
        string ComputeSha256Hash(string password);
    }
}
