using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace AWS.SignatureV4.Generator
{
    public static class Helpers
    {
        public static string SHA256(this string value)
        {
            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            return Sb.ToString();
        }
    }
}
