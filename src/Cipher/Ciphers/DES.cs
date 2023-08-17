using System.Security.Cryptography;
using System.Text;
using Hopex.Cipher.Interfaces;

namespace Hopex.Cipher.Ciphers;

internal class DES : ICipher
{
    private const CipherMode CipherMode = System.Security.Cryptography.CipherMode.ECB;

    private const PaddingMode PaddingMode = System.Security.Cryptography.PaddingMode.PKCS7;
    private readonly string _key;

    public DES(string key)
    {
        if (string.IsNullOrEmpty(key))
            throw new Exception("The encryption key cannot be empty.");

        _key = key;
    }

    /// <inheritdoc />
    public string Encode(string input)
    {
        var bytesOfInput = Encoding.UTF8.GetBytes(input);
        using var mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
        var bytesOfHash = mD5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(_key));
        using var tripleCryptoServiceProvider = new TripleDESCryptoServiceProvider();
        tripleCryptoServiceProvider.Key = bytesOfHash;
        tripleCryptoServiceProvider.Mode = CipherMode;
        tripleCryptoServiceProvider.Padding = PaddingMode;

        var bytesOfTransformBlock = tripleCryptoServiceProvider
            .CreateEncryptor()
            .TransformFinalBlock(
                bytesOfInput,
                0,
                bytesOfInput.Length
            );

        return Convert.ToBase64String(
            bytesOfTransformBlock,
            0,
            bytesOfTransformBlock.Length
        );
    }

    /// <inheritdoc />
    public string Decode(string input)
    {
        var bytesOfInput = Convert.FromBase64String(input);
        using var mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
        var bytesOfHash = mD5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(_key));
        using var tripDes = new TripleDESCryptoServiceProvider();
        tripDes.Key = bytesOfHash;
        tripDes.Mode = CipherMode;
        tripDes.Padding = PaddingMode;

        var cryptoTransform = tripDes.CreateDecryptor();
        var bytesOfTransformBlock = cryptoTransform.TransformFinalBlock(
            bytesOfInput,
            0,
            bytesOfInput.Length
        );
        return Encoding.UTF8.GetString(bytesOfTransformBlock);
    }
}