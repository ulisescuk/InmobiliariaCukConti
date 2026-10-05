using CukConti.Application.Interfaces;

namespace CukConti.Infrastructure.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Hashear(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool Verificar(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
