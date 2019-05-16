using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services.Auth
{
    public class JWTValidationParameters : TokenValidationParameters
    {
        public JWTValidationParameters()
        {
            ValidateIssuer = false;
            ValidIssuer = AuthOptions.Issuer;

            ValidateAudience = false;
            ValidAudience = AuthOptions.Audience;

            ValidateIssuerSigningKey = true;
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey();

            ValidateLifetime = false;
        }
    }
}
