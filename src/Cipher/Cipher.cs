using Hopex.Cipher.Enums;
using Hopex.Cipher.Interfaces;
using Hopex.Cipher.Ciphers;

namespace Hopex.Cipher
{
    /// <summary>
    /// Provides string encryption and decryption.
    /// </summary>
    public class Cipher : ICipher
    {
        private readonly ICipher resolveCipher;

        /// <summary>
        /// Provides string encryption and decryption.
        /// </summary>
        /// <param name="type">Type of encryption.</param>
        /// <param name="key">Required for DES cipher.</param>
        public Cipher(CipherType type, string key = default)
        {
            switch (type)
            {
                case CipherType.AES:
                    resolveCipher = new AES();
                    break;
                case CipherType.Base16:
                    resolveCipher = new Base16();
                    break;
                case CipherType.Base64:
                    resolveCipher = new Base64();
                    break;
                case CipherType.DES:
                    resolveCipher = new DES(key);
                    break;
                case CipherType.MD5:
                    resolveCipher = new MD5();
                    break;
                case CipherType.SHA1:
                    resolveCipher = new SHA1();
                    break;
                case CipherType.SHA256:
                    resolveCipher = new SHA256();
                    break;
            }
        }

        /// <inheritdoc/>
        public string Encode(string input) => resolveCipher.Encode(input);

        /// <inheritdoc/>
        public string Decode(string input) => resolveCipher.Decode(input);
    }
}
