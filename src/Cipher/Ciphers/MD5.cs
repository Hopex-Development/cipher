using System;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

using Hopex.Cipher.Interfaces;

namespace Hopex.Cipher.Ciphers
{
    internal class MD5 : ICipher
    {
        /// <inheritdoc/>
        public string Encode(string input)
        {
            using (var cryptoServiceProvider = new MD5CryptoServiceProvider())
            {
                byte[] bytesOfInput = Encoding.UTF8.GetBytes(input);
                StringBuilder stringBuilder = new StringBuilder();
                foreach (byte x in cryptoServiceProvider.ComputeHash(bytesOfInput))
                    stringBuilder.Append(x.ToString("x2"));

                return stringBuilder.ToString();
            }
        }

        /// <inheritdoc/>
        public string Decode(string input)
        {
            throw new Exception("MD5 hash cannot be decoded.");
        }
    }
}
