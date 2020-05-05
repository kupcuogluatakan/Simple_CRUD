using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ODMSCommon.Security
{
    public class PasswordSecurityProvider
    {
        private const string Salt = "dAk9879*&@p";
        private readonly HashAlgorithm _hashAlgorithm= new SHA512Managed();
        public string GenerateHashedPassword(string plainPassword)
        {
            byte[] passwordBytes = Encoding.ASCII.GetBytes(plainPassword);
            byte[] saltBytes = Encoding.ASCII.GetBytes(Salt);
            byte[] plainTextWithSaltBytes =new byte[plainPassword.Length + Salt.Length];
            for (int i = 0; i < passwordBytes.Length; i++)
            {
                plainTextWithSaltBytes[i] = passwordBytes[i];
            }
            for (int i = 0; i < saltBytes.Length; i++)
            {
                plainTextWithSaltBytes[passwordBytes.Length + i] = saltBytes[i];
            }
            var hashedBytes=_hashAlgorithm.ComputeHash(plainTextWithSaltBytes);
            return Convert.ToBase64String(hashedBytes);
        }
        public bool CompareHashed(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
