using System.Security.Cryptography;
using System.Text;
using Hopex.Cipher.Interfaces;

namespace Hopex.Cipher.Ciphers;

internal class AES : ICipher
{
    private readonly byte[] _key = Enumerable.Range(
        0,
        32
    ).Select(x => (byte)x).ToArray();

    /// <inheritdoc />
    public string Encode(string input)
    {
        using var memoryStream = new MemoryStream();
        using (var aesManaged = new AesManaged())
        {
            aesManaged.Key = _key;
            memoryStream.Write(
                aesManaged.IV,
                0,
                aesManaged.IV.Length
            );
            using var cryptoStream = new CryptoStream(
                memoryStream,
                aesManaged.CreateEncryptor(),
                CryptoStreamMode.Write
            );
            var bytesOfInput = Encoding.UTF8.GetBytes(input);
            cryptoStream.Write(
                bytesOfInput,
                0,
                bytesOfInput.Length
            );
        }

        return Convert.ToBase64String(memoryStream.ToArray());
    }

    /// <inheritdoc />
    public string Decode(string input)
    {
        using var memoryStream = new MemoryStream(Convert.FromBase64String(input));
        var iv = new byte[16];
        memoryStream.Read(
            iv,
            0,
            iv.Length
        );

        using var aesManaged = new AesManaged();
        aesManaged.Key = _key;
        aesManaged.IV = iv;
        using var cryptoStream = new CryptoStream(
            stream: memoryStream,
            transform: aesManaged.CreateDecryptor(),
            mode: CryptoStreamMode.Read
        );
        using var outputMemoryStream = new MemoryStream();
        cryptoStream.CopyTo(outputMemoryStream);
        return Encoding.UTF8.GetString(outputMemoryStream.ToArray());
    }
}