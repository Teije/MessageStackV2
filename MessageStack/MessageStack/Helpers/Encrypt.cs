using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MessageStack.Helpers
{
    public static class Encrypt
    {
        public static string GenerateSHA512String(string inputString)
        {
            var sha512 = SHA512.Create();
            var bytes = Encoding.UTF8.GetBytes(inputString);
            var hash = sha512.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        private static string GetStringFromHash(IEnumerable<byte> hash)
        {
            var result = new StringBuilder();
            foreach (var t in hash)
            {
                result.Append(t.ToString("X2"));
            }
            return result.ToString();
        }
    }
}