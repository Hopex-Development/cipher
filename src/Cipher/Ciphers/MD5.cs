using System.Security.Cryptography;
using System.Text;
using Hopex.Cipher.Interfaces;

namespace Hopex.Cipher.Ciphers;

internal class MD5(string key = default) : ICipher
{
    private readonly byte[] _key = key == default ? Array.Empty<byte>() : Encoding.UTF8.GetBytes((string)key);

    /// <inheritdoc />
    public string Encode(string input)
    {
        var bytesOfInput = Encoding.UTF8.GetBytes(input);
        byte[] hash;

        if (key.Length == 0)
        {
            using var cryptoServiceProvider = new MD5CryptoServiceProvider();
            hash = cryptoServiceProvider.ComputeHash(bytesOfInput);
        }
        else
        {
            using var md5 = new HMACMD5(_key);
            hash = md5.ComputeHash(bytesOfInput);
        }

        var stringBuilder = new StringBuilder();
        foreach (var x in hash)
            stringBuilder.Append(x.ToString("x2"));

        return stringBuilder.ToString();
    }

    /// <inheritdoc />
    public string Decode(string input)
    {
        throw new Exception("MD5 hash cannot be decoded.");
    }
}