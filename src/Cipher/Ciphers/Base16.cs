using System;
using System.Linq;
using System.Text;

using Hopex.Cipher.Interfaces;

namespace Hopex.Cipher.Ciphers
{
    internal class Base16 : ICipher
    {
        /// <inheritdoc/>
        public string Encode(string input)
        {
            return string.Concat(
                    values: Encoding.Unicode
                        .GetBytes(s: input)
                        .Select(selector: x => x.ToString(format: "x2"))
                );
        }

        /// <inheritdoc/>
        public string Decode(string input)
        {
            byte[] bytesOfIpnut = new byte[input.Length / 2];
            for (var i = 0; i < bytesOfIpnut.Length; i++)
                bytesOfIpnut[i] = Convert.ToByte(
                    value: input.Substring(
                        startIndex: i * 2,
                        length: 2
                    ), 
                    fromBase: 16
                );

            return Encoding.Unicode.GetString(bytes: bytesOfIpnut);
        }
    }
}
