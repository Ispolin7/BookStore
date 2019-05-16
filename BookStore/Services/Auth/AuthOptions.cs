using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BookStore.Services.Auth
{
    public static class AuthOptions
    {
        public const string Issuer = "BookStoreApp";
        public const string Audience = "https://localhost:44365";
        private const string SecurityKey = "MySuperSecretApplicationKey";

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecurityKey));
        }
    }
}
