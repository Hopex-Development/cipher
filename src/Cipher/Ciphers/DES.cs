using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Hopex.Cipher.Interfaces;

namespace Hopex.Cipher.Ciphers
{
    internal class DES : ICipher
    {
        private readonly string key;

        private readonly CipherMode cipherMode = CipherMode.ECB;

        private readonly PaddingMode paddingMode = PaddingMode.PKCS7;

        public DES(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new Exception("The encryption key cannot be empty.");

            this.key = key;
        }

        /// <inheritdoc/>
        public string Encode(string input)
        {
            byte[] bytesOfInput = Encoding.UTF8.GetBytes(s: input);
            using (MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider())
            {
                byte[] bytesOfHash = mD5CryptoServiceProvider.ComputeHash(buffer: Encoding.UTF8.GetBytes(s: key));
                using (TripleDESCryptoServiceProvider tripleCryptoServiceProvider = new TripleDESCryptoServiceProvider()
                { 
                    Key = bytesOfHash, 
                    Mode = cipherMode, 
                    Padding = paddingMode
                })
                {
                    byte[] bytesOfTransformBlock = tripleCryptoServiceProvider
                        .CreateEncryptor()
                        .TransformFinalBlock(
                            inputBuffer: bytesOfInput,
                            inputOffset: 0,
                            inputCount: bytesOfInput.Length
                        );

                    return Convert.ToBase64String(
                        inArray: bytesOfTransformBlock, 
                        offset: 0, 
                        length: bytesOfTransformBlock.Length
                    );
                }
            }
        }

        /// <inheritdoc/>
        public string Decode(string input)
        {
            byte[] bytesOfInput = Convert.FromBase64String(s: input);
            using (MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider())
            {
                byte[] bytesOfHash = mD5CryptoServiceProvider.ComputeHash(buffer: Encoding.UTF8.GetBytes(s: key));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() 
                { 
                    Key = bytesOfHash, 
                    Mode = cipherMode, 
                    Padding = paddingMode
                })
                {
                    ICryptoTransform cryptoTransform = tripDes.CreateDecryptor();
                    byte[] bytesOfTransformBlock = cryptoTransform.TransformFinalBlock(
                        inputBuffer: bytesOfInput,
                        inputOffset: 0,
                        inputCount: bytesOfInput.Length
                    );
                    return Encoding.UTF8.GetString(bytes: bytesOfTransformBlock);
                }
            }
        }
    }
}
