using JobApplication.Services.Interfaces;
using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace JobApplication.Services
{
    public class CryptoService : ICryptoService
    {
        private readonly string _encryptionKey;

        public CryptoService()
        {
            _encryptionKey = ConfigurationManager.AppSettings["EncryptionKey"];
            if (string.IsNullOrWhiteSpace(_encryptionKey))
            {
                throw new Exception("Encryption key is missing from Web.config.");
            }
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }

        public string Encrypt(string plainText)
        {
            byte[] key = Encoding.UTF8.GetBytes(_encryptionKey.Substring(0, 16));
            using (var aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = key;

                using (var encryptor = aes.CreateEncryptor())
                {
                    var inputBytes = Encoding.UTF8.GetBytes(plainText);
                    var encrypted = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);
                    return Convert.ToBase64String(encrypted);
                }

            }
        }

        public string Decrypt(string cipherText)
        {
            byte[] key = Encoding.UTF8.GetBytes(_encryptionKey.Substring(0, 16));
            using (var aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = key;

                using (var decryptor = aes.CreateDecryptor())
                {
                    var encryptedBytes = Convert.FromBase64String(cipherText);
                    var decrypted = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                    return Encoding.UTF8.GetString(decrypted);
                }

            }
        }

        public string HashSha256(string plaintext)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(plaintext);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}