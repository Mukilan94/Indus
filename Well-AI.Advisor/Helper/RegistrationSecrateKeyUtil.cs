using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace WellAI.Advisor.Helper
{
    public  class RegistrationSecrateKeyUtil
    {
        byte[] randomBytes = new Byte[64];

        public  string GenerateKey()
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
