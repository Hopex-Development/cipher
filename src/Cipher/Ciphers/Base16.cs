using System.Text;
using Hopex.Cipher.Interfaces;

namespace Hopex.Cipher.Ciphers;

internal class Base16 : ICipher
{
    /// <inheritdoc />
    public string Encode(string input)
    {
        return string.Concat(
            Encoding.Unicode
                .GetBytes(input)
                .Select(x => x.ToString("x2"))
        );
    }

    /// <inheritdoc />
    public string Decode(string input)
    {
        var bytesOfIpnut = new byte[input.Length / 2];
        for (var i = 0; i < bytesOfIpnut.Length; i++)
            bytesOfIpnut[i] = Convert.ToByte(
                input.Substring(
                    i * 2,
                    2
                ),
                16
            );

        return Encoding.Unicode.GetString(bytesOfIpnut);
    }
}