
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace PagoElectronico.Commons
{
    class Sha256Generator
    {
        public string GetHashString(string stringToHash) {
            byte[] passwordSha256 = new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(stringToHash));
            string result = BitConverter.ToString(passwordSha256)
                .Replace("-", string.Empty)
                .ToLower();
            return result;
        }
    }
}
