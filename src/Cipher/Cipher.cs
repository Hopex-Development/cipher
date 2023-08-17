using Hopex.Cipher.Ciphers;
using Hopex.Cipher.Enums;
using Hopex.Cipher.Interfaces;

namespace Hopex.Cipher;

/// <summary>
///     Provides string encryption and decryption.
/// </summary>
public class Cipher : ICipher
{
    private readonly ICipher _resolveCipher;

    /// <summary>
    ///     Provides string encryption and decryption.
    /// </summary>
    /// <param name="type">Type of encryption.</param>
    /// <param name="key">Required for DES cipher.</param>
    public Cipher(CipherType type, string key = default)
    {
        _resolveCipher = type switch
        {
            CipherType.AES => new AES(key),
            CipherType.Base16 => new Base16(),
            CipherType.Base32 => new Base32(),
            CipherType.Base64 => new Base64(),
            CipherType.DES => new DES(key),
            CipherType.MD5 => new MD5(key),
            CipherType.SHA1 => new SHA1(),
            CipherType.SHA256 => new SHA256(),
            _ => _resolveCipher
        };
    }

    /// <inheritdoc />
    public string Encode(string input)
    {
        return _resolveCipher.Encode(input);
    }

    /// <inheritdoc />
    public string Decode(string input)
    {
        return _resolveCipher.Decode(input);
    }
}