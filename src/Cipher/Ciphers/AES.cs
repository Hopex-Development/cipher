using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using Hopex.Cipher.Interfaces;

namespace Hopex.Cipher.Ciphers
{
    internal class AES : ICipher
    {
        private readonly byte[] key = Enumerable.Range(
            start: 0, 
            count: 32
        ).Select(selector: x => (byte)x).ToArray();

        /// <inheritdoc/>
        public string Encode(string input)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (AesManaged aesManaged = new AesManaged()
                {
                    Key = key 
                })
                {
                    memoryStream.Write(
                        buffer: aesManaged.IV,
                        offset: 0,
                        count: aesManaged.IV.Length
                    );
                    using (CryptoStream cryptoStream = new CryptoStream(
                        stream: memoryStream,
                        transform: aesManaged.CreateEncryptor(),
                        mode: CryptoStreamMode.Write,
                        leaveOpen: true
                    ))
                    {
                        byte[] bytesOfInput = Encoding.UTF8.GetBytes(s: input);
                        cryptoStream.Write(
                            buffer: bytesOfInput, 
                            offset: 0,
                            count: bytesOfInput.Length
                        );
                    }
                }

                return Convert.ToBase64String(inArray: memoryStream.ToArray());
            }
        }

        /// <inheritdoc/>
        public string Decode(string input)
        {
            using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(s: input)))
            {
                byte[] iv = new byte[16];
                memoryStream.Read(
                    buffer: iv,
                    offset: 0,
                    count: iv.Length
                );

                using (AesManaged aesManaged = new AesManaged()
                { 
                    Key = key, 
                    IV = iv
                })
                using (CryptoStream cryptoStream = new CryptoStream(
                    stream: memoryStream, 
                    transform: aesManaged.CreateDecryptor(), 
                    mode: CryptoStreamMode.Read, 
                    leaveOpen: true
                ))
                using (MemoryStream outputMemoryStream = new MemoryStream())
                {
                    cryptoStream.CopyTo(destination: outputMemoryStream);
                    return Encoding.UTF8.GetString(outputMemoryStream.ToArray());
                }
            }
        }
    }
}
