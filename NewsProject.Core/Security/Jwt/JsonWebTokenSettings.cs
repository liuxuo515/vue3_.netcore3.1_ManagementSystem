using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessAccount.Security.Jwt
{
    public class JsonWebTokenSettings
    {
        public JsonWebTokenSettings(string key, TimeSpan expires)
        {
            SecurityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(key));
            Expires = expires;
        }
        public JsonWebTokenSettings(string key, TimeSpan expires, string audience, string issuer)
            : this(key, expires)
        {
            Audience = audience;
            Issuer = issuer;
        }

        public string Audience { get; }
        public TimeSpan Expires { get; }
        public string Issuer { get; }
        public SecurityKey SecurityKey { get; }

        public TokenValidationParameters TokenValidationParameters()
        {
            return new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = SecurityKey,
                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = !string.IsNullOrEmpty(Issuer),
                ValidIssuer = Issuer,
                // Validate the JWT Audience (aud) claim
                ValidateAudience = !string.IsNullOrEmpty(Audience),
                ValidAudience = Audience,

                ValidateLifetime = true,
                // If you want to allow a certain amount of clock drift, set thatValidIssuer  here
                ClockSkew = TimeSpan.Zero
            };
        }
    }
}
