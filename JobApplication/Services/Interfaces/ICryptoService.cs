namespace JobApplication.Services.Interfaces
{
    public interface ICryptoService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);
        string Encrypt(string plainText);
        string Decrypt(string cipherText);

        string HashSha256(string plaintext);
    }
}
