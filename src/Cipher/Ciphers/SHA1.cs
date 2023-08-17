﻿using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using Hopex.Cipher.Interfaces;

namespace Hopex.Cipher.Ciphers
{
    internal class SHA1 : ICipher
    {
        /// <inheritdoc/>
        public string Encode(string input)
        {
            return string.Concat(
                values: new SHA1Managed()
                    .ComputeHash(buffer: Encoding.UTF8.GetBytes(s: input))
                    .Select(selector: x => x.ToString(format: "x2"))
                );
        }

        /// <inheritdoc/>
        public string Decode(string input)
        {
            throw new Exception(message: "SHA1 hash cannot be decoded.");
        }
    }
}