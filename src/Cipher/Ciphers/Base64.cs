using System.Text;
using Hopex.Cipher.Interfaces;

namespace Hopex.Cipher.Ciphers;

internal class Base64 : ICipher
{
    /// <inheritdoc />
    public string Encode(string input)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
    }

    /// <inheritdoc />
    public string Decode(string input)
    {
        return Encoding.UTF8.GetString(Convert.FromBase64String(input));
    }
}