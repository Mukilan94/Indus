using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace WellAI.Advisor.API.Dispatch.Helpers
{
    public class SecretKeyUtils
    {
        byte[] randomBytes = new Byte[64];

        public static string GenerateKey()
        {
            byte[] randomBytes = new Byte[8];
            using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }
            SHA256 ShaHashFunction = SHA256.Create();
            byte[] hashedBytes = ShaHashFunction.ComputeHash(randomBytes);
            string randomString = string.Empty;
            randomString = BitConverter.ToString(hashedBytes).Replace("-", "");
            return randomString;
        }

        
    }
}
