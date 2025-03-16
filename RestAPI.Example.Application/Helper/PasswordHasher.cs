

using System.Security.Cryptography;

namespace RestAPI.Example.Application.Helper
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 100000;

        private static readonly HashAlgorithmName AlgorithmName = HashAlgorithmName.SHA512;

        public string Hash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, AlgorithmName, HashSize);

            return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
        }

        public bool Verify(string hashedPassword, string password)
        {
            var parts = hashedPassword.Split('-');
            var storedHash = Convert.FromHexString(parts[0]);
            var storedSalt = Convert.FromHexString(parts[1]);

            var hash = Rfc2898DeriveBytes.Pbkdf2(password, storedSalt, Iterations, AlgorithmName, HashSize);

            return CryptographicOperations.FixedTimeEquals(hash, storedHash); 
        }
    }
}
