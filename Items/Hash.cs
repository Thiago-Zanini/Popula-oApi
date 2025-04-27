using Microsoft.AspNetCore.Identity;

namespace Populacao.Items
{
    public class Hash
    {
        private readonly IPasswordHasher<object> hash;

        public Hash()
        {
            this.hash = new PasswordHasher<object>();
        }

        public string Hashar(string Senha)
        {
            return hash.HashPassword(null, Senha);
        }

        public bool verify(string Senha, string SenhaHash)
        {
            var verificar = hash.VerifyHashedPassword(null, SenhaHash, Senha);
            return verificar == PasswordVerificationResult.Success;
        }
    }
}
